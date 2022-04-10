export { loadItem2 }
import { removeItem } from "./dashboard.js"
import { getItem2 } from "./dashboardapi.js"



async function loadItem2() {

    (document.getElementById("i2refresh") as HTMLElement).addEventListener("click", () => {
        refresh();
    });
    (document.getElementById("i2close") as HTMLElement).addEventListener("click", () => {
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
    let response = await getItem2(data);
    toggleClassOff("item2spinner", "fa-spin");
    if (response != null) {
        container.value = response.data;
    }
    toggleClassOn("item2spinner", "w3-hide");
    toggleClassOff("item2container", "w3-hide");
}

