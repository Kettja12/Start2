export { loadItem3 }
import { removeItem } from "./dashboard.js"
import { getItem3 } from "./dashboardapi.js"

async function loadItem3() {
    (<HTMLElement> document.getElementById("i3refresh")).addEventListener("click", () => {
        refresh();
    });
    (<HTMLElement> document.getElementById("i3close")).addEventListener("click", () => {
        removeItem('Item3');
    });
    refresh();
}
async function refresh() {
    let data: Item3Type = {
        a: (document.getElementById('i3A') as HTMLInputElement).value,
        b: (document.getElementById('i3B') as HTMLInputElement).value,
        result:""
    };
    toggleClassOff("item3spinner", "w3-hide");
    toggleClassOn("item3container", "w3-hide");
    toggleClassOn("item3spinner", "fa-spin");
    let response = await getItem3(data);
    toggleClassOff("item3spinner", "fa-spin");
    if (response != null) {
        (document.getElementById('i3A') as HTMLInputElement).value = response.a;
        (document.getElementById('i3B') as HTMLInputElement).value = response.b;
        (document.getElementById('i3Result') as HTMLInputElement).value = response.result;
        toggleClassOn("item3spinner", "w3-hide");
        toggleClassOff("item3container", "w3-hide");

    }
}

