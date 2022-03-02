/// <reference path="../node_modules/@types/chart.js/index.d.ts" />
loadScript('../scripts/chart.js', undefined, undefined);
async function loadItem1() {
    let data = {
    };
    toggleClassOff("item1spinner", "w3-hide");
    toggleClassOn("item1container", "w3-hide");
    toggleClassOn("item1spinner", "fa-spin");
    let response = await apiPost("Dashboard/GetItem1", data);
    toggleClassOff("item1spinner", "fa-spin");
    if (response.status === "OK") {
        var xValues = ["Q1", "Q2","Q3","Q4"];
        var yValues1 = [];
        var yValues2 = [];
        let graphdata = <Item1Type>response.data;
        for (let i = 0; i < 4; i++) {
            yValues1.push(graphdata.renevue[i]);
            yValues2.push(graphdata.renevue2[i]);
        }
        new Chart("item1Chart", {
            type: "bar",

            data: {
                labels: xValues,
                datasets: [{
                    label: "2020",
                    backgroundColor: "green",
                    data: yValues1
                },
                {
                    label: "2021",
                    backgroundColor: "blue",
                    data: yValues2
                }]
            },
            options: {
            }
        });
        toggleClassOn("item1spinner", "w3-hide");
        toggleClassOff("item1container", "w3-hide");

    }
}
