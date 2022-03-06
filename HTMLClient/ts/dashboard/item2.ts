export { loadItem2 }
import { removeItem } from "./dashboard.js"



async function loadItem2() {

    document.getElementById("i2refresh").addEventListener("click", () => {
        refresh();
    });
    document.getElementById("i2close").addEventListener("click", () => {
        removeItem('Item2');
    });
    refresh();
}
async function refresh() {

    let container = <HTMLInputElement>document.getElementById('username');
    let data = {
        data: container.value
    };
    toggleClassOff("item2spinner", "w3-hide");
    toggleClassOn("item2container", "w3-hide");
    toggleClassOn("item2spinner", "fa-spin");
    let response = await apiPost("Dashboard/GetItem2", data);
    toggleClassOff("item2spinner", "fa-spin");
    if (response.status === "OK") {
        container.value = response.data.data;
        toggleClassOn("item2spinner", "w3-hide");
        toggleClassOff("item2container", "w3-hide");
    }
}

