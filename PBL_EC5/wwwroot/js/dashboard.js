const ip = "34.231.100.192";

const headers = {
    "fiware-service": "smart",
    "fiware-servicepath": "/",
};

// Detecta o protocolo atual (http ou https)
const protocol = window.location.protocol === "https:" ? "https" : "http";

// Monta o URL com o protocolo correto
const url = `${protocol}://${ip}:8666/STH/v1/contextEntities/type/Lamp/id/urn:ngsi-ld:Lamp:001/attributes/temperature?lastN=30`;

// Função para chamar o endpoint e exibir a data no console
function fetchTemperatureData() {
    $.ajax({
        url: url,
        method: "GET",
        headers: headers,
        success: function (response) {
            if (response && response.contextResponses && response.contextResponses.length > 0) {
                const data = response.contextResponses[0].contextElement.attributes[0].values;
                console.log("Data:", data);
            } else {
                console.log("Nenhum dado encontrado.");
            }
        },
        error: function (error) {
            console.error("Erro ao buscar os dados:", error);
        }
    });
}

// Chamar a função
fetchTemperatureData();