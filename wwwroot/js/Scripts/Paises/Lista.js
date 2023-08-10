$(document).ready(function () {
    new DataTable('#tablePaises', {
        "ajax": {
            "url": "/Paises/JsSearch",
            "type": "GET",
            "dataType": "json"
        },
        columns: [
            { data: 'nmPais' },
            { data: 'DDI' },
            { data: 'sigla' },
            { data: '' }
        ]
    });
});