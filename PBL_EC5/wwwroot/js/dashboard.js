var dashboard = function () {

    var init = function () {
        console.log("Inicializando o dashboard...");
        fetchTemperatureData();
    }

    const ip = "34.231.100.192";

    const headers = {
        "fiware-service": "smart",
        "fiware-servicepath": "/",
    };

    // Monta o URL com o protocolo correto
    const url = `http://${ip}:8666/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:001/attributes/temperature?lastN=30`;

    // Função para chamar o endpoint e exibir a data no console
    var fetchTemperatureData = function () {

        console.log("Buscando dados de temperatura...");

        $.ajax({
            url: "/DadosEstufa/BuscarTemperatura",
            method: "GET",
            headers: headers,
            dataType: "json",
            accepts: "application/json",
            success: function (response) {
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
                    sendDataToController(formattedData);
                } else {
                    console.log("Nenhum dado encontrado.");
                }
            },
            error: function (error) {
                console.error("Erro ao buscar os dados:", error);
            }
        });
    }

    function sendDataToController(data) {
        $.ajax({
            url: "/DadosEstufa/SalvarDados", // Endpoint da controller
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                console.log("Dados enviados com sucesso:", response);
            },
            error: function (error) {
                console.error("Erro ao enviar os dados:", error);
            }
        });
    }

    return {
        init: init,
        fetchTemperatureData: fetchTemperatureData
    };
}()