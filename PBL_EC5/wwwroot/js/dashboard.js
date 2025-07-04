﻿var dashboard = function () {
    var listadados = []; // Armazena todos os dados recebidos
    var myChart = null;  // Referência ao gráfico Chart.js
    var myChartHistorico = null;
    var temperaturaAtual = null;
    var setPoint = null;

    var init = async function () {
        console.log("Inicializando o dashboard...");
        montaGrafico(listadados);
        await fetchTemperatureData();
        setInterval(fetchTemperatureData, 500);
    }

    const headers = {
        "fiware-service": "smart",
        "fiware-servicepath": "/",
    };

    // Função para chamar o endpoint e exibir a data no console
    var fetchTemperatureData = async function () {
        var estufaId = $("#EstufaId").val();

        try {
            const response = await new Promise(function (resolve, reject) {
                $.ajax({
                    url: "/DadosEstufa/BuscarTemperatura",
                    method: "POST",
                    headers: headers,
                    data: { estufaId: estufaId },
                    dataType: "json",
                    accepts: "application/json",
                    success: resolve,
                    error: reject
                });
            });

            if (response && response.contextResponses && response.contextResponses.length > 0) {
                const data = response.contextResponses[0].contextElement.attributes[0].values;

                // Formata os dados para enviar à controller
                const formattedData = data.map(item => ({
                    Id: item._id,
                    RecvTime: item.recvTime,
                    AttrName: item.attrName,
                    AttrType: item.attrType,
                    AttrValue: item.attrValue
                }));

                // Envia os dados para a controller
                await sendDataToController(formattedData);

            } else {
                console.log("Nenhum dado encontrado.");
            }
        } catch (error) {
            console.error("Erro ao buscar os dados:", error);
        }
    }

    function sendDataToController(data) {
        $.ajax({
            url: "/DadosEstufa/SalvarDados",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                IdEstufa: parseInt($("#EstufaId").val()),
                Dados: data
            }),
            success: function (response) {
                //Atualiza o gráfico se salvou com sucesso
                data.forEach(item => {
                    if (!listadados.some(d => d.Id === item.Id)) {
                        listadados.push(item);
                    }
                });

                if (myChart) {
                    labels = listadados.map(item => {
                        const date = new Date(item.RecvTime);
                        return date.toLocaleString('pt-BR', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
                    });
                    temperaturas = listadados.map(item => parseFloat(item.AttrValue));

                    myChart.data.labels = labels;
                    myChart.data.datasets[0].data = temperaturas;
                    myChart.update();
                }

                // Atualiza a cor da div de status de temperatura
                if (listadados.length > 0) {
                    const ultimaTemp = parseFloat(listadados[listadados.length - 1].AttrValue);
                    const tempDiv = document.getElementById('tempStatus');
                    tempDiv.textContent = isNaN(ultimaTemp) ? '--' : ultimaTemp + '°C';

                    // Defina os limites de cor conforme sua necessidade
                    if (isNaN(ultimaTemp)) {
                        tempDiv.style.background = '#ccc';
                    } else if (ultimaTemp < 18) {
                        tempDiv.style.background = '#00bfff'; // Azul (frio)
                    } else if (ultimaTemp < 28) {
                        tempDiv.style.background = '#90ee90'; // Verde (confortável)
                    } else if (ultimaTemp < 35) {
                        tempDiv.style.background = '#ffd700'; // Amarelo (quente)
                    } else {
                        tempDiv.style.background = '#ff6347'; // Vermelho (muito quente)
                    }
                }

                // Atualiza o erro estacionário
                temperaturaAtual = parseFloat(listadados[listadados.length - 1].AttrValue);
                atualizarErroEstacionario();

                console.log("Dados enviados com sucesso:", response);
            },
            error: function (error) {
                console.error("Erro ao enviar os dados:", error);
            }
        });
    }

    function montaGrafico(data) {
        // Prepara os dados para o gráfico
        const labels = data.map(item => {
            const date = new Date(item.RecvTime);
            return date.toLocaleString('pt-BR', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
        });
        const temperaturas = data.map(item => parseFloat(item.AttrValue));

        // Remove gráfico anterior, se existir
        if (myChart) {
            myChart.destroy();
        }

        const ctx = document.getElementById('myChart').getContext('2d');
        myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Temperatura (°C)',
                    data: temperaturas,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    fill: true,
                    tension: 0.1,
                    pointRadius: 1
                }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Horário'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Temperatura (°C)'
                        },
                        beginAtZero: false
                    }
                }
            }
        });
    }

    // Função para atualizar o erro estacionário
    function atualizarErroEstacionario() {
        const erroDiv = document.getElementById('erroEstacionario');
        if (erroDiv != null || erroDiv != undefined) {
            if (setPoint !== null && temperaturaAtual !== null && !isNaN(setPoint) && !isNaN(temperaturaAtual)) {
                const erro = setPoint - temperaturaAtual;
                erroDiv.textContent = `${erro.toFixed(2)}`;
            } else {
                erroDiv.textContent = '';
            }
        }
    }

    // Evento para capturar mudanças no setPoint
    document.addEventListener('DOMContentLoaded', function () {
        const setPointInput = document.getElementById('setPointInput');
        if (setPointInput) {
            setPointInput.addEventListener('input', function () {
                setPoint = parseFloat(this.value);
                atualizarErroEstacionario();
            });
        }
    });

    //GRÁFICO HISTÓRICO
    $(document).ready(function () {
        $("#btnBuscarHistorico").click(async function () {
            const idEstufa = $("#EstufaId").val();
            const dataInicial = new Date($("#dataInicial").val()).toISOString();
            const dataFinal = new Date($("#dataFinal").val()).toISOString();

            if (!dataInicial || !dataFinal) {
                alert("Selecione o intervalo de datas.");
                return;
            }

            try {
                const registros = await buscarHistorico(idEstufa, dataInicial, dataFinal);
                montaGraficoHistorico(registros);
            } catch (e) {
                alert("Erro ao buscar histórico.");
            }
        });
    });

    async function buscarHistorico(idEstufa, dataInicial, dataFinal) {
        return await new Promise(function (resolve, reject) {
            $.ajax({
                url: "/DadosEstufa/BuscarHistorico",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify({
                    IdEstufa: parseInt(idEstufa),
                    DataInicial: dataInicial,
                    DataFinal: dataFinal
                }),
                success: resolve,
                error: reject
            });
        });
    }

    function montaGraficoHistorico(data) {
        // Ordena os dados por data
        const dadosOrdenados = data.slice().sort((a, b) => new Date(a.Data) - new Date(b.Data));
        const labels = dadosOrdenados.map(item => {
            const date = new Date(item.data);
            return date.toLocaleString('pt-BR', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
        });
        const temperaturas = dadosOrdenados.map(item => parseFloat(item.temperatura));

        // Remove gráfico anterior, se existir
        if (myChartHistorico) {
            myChartHistorico.destroy();
        }

        const ctx = document.getElementById('myChartHistorico').getContext('2d');
        myChartHistorico = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Temperatura (°C) - Histórico',
                    data: temperaturas,
                    borderColor: 'rgba(255, 99, 132, 1)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    fill: true,
                    tension: 0.1,
                    pointRadius: 1
                }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Horário'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Temperatura (°C)'
                        },
                        beginAtZero: false
                    }
                }
            },
        });
    }

    return {
        init: init,
        fetchTemperatureData: fetchTemperatureData
    };
}()