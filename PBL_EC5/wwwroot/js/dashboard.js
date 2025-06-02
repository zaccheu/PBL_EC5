var dashboard = function () {
    var listadados = []; // Armazena todos os dados recebidos
    var myChart = null;  // Referência ao gráfico Chart.js

    var init = async function () {
        console.log("Inicializando o dashboard...");
        await fetchTemperatureData();
        montaGrafico(listadados);
        setInterval(fetchTemperatureData, 2000);
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

    return {
        init: init,
        fetchTemperatureData: fetchTemperatureData
    };
}()