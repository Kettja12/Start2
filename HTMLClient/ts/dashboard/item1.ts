/// <reference path="../node_modules/@types/chart.js/index.d.ts" />
/// <reference path="./dashboard.ts" />
loadScript('../scripts/chart.js', undefined, undefined);
//export { loadItem1 }
//import { Chart } from "chart.js"
//import { getItem1 } from "./dashboardapi.js"
let item1message: string = "";

//import { removeItem } from "./dashboard.js"
let chart: Chart|null = null;
async function loadItem1() {
    (document.getElementById("i1refresh") as HTMLElement).addEventListener("click", () => {
        refresh();
    });
    (document.getElementById("i1close") as HTMLElement).addEventListener("click", () => {
        window.removeItem('Item1');
    });
    refresh();
}

async function refresh() {
    toggleClassOff("item1spinner", "w3-hide");
    toggleClassOn("item1container", "w3-hide");
    toggleClassOn("item1spinner", "fa-spin");
    let graphdata = await getItem1();
    toggleClassOff("item1spinner", "fa-spin");
    if (graphdata !== null)
    {
        var xValues = ["Q1", "Q2","Q3","Q4"];
        var yValues1:number[] = [];
        var yValues2: number[] = [];
        for (let i = 0; i < 4; i++) {
            yValues1.push(<number>graphdata.renevue[i]);
            yValues2.push(<number> graphdata.renevue2[i]);
        }
        let ctx = <HTMLCanvasElement>document.getElementById("item1Chart");
        if (chart !== null) chart.destroy();
        chart = new Chart(ctx, {
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
async function getItem1():
    Promise<Item1Type | null> {
    let response = await apiPost("Dashboard/GetItem1", {});
    if (response.status === "OK") {
        return <Item1Type>response.data;
    }
    return null;
}
