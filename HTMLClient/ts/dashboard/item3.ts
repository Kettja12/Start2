export { loadItem3 }
import { removeItem } from "./dashboard.js"

async function loadItem3() {
    document.getElementById("i3refresh").addEventListener("click", () => {
        loadItem3();
    });
    document.getElementById("i3close").addEventListener("click", () => {
        removeItem('Item3');
    });
    let data: Item3Type = {
        a: (document.getElementById('i3A') as HTMLInputElement).value,
        b: (document.getElementById('i3B') as HTMLInputElement).value,
        result:""
    };
    toggleClassOff("item3spinner", "w3-hide");
    toggleClassOn("item3container", "w3-hide");
    toggleClassOn("item3spinner", "fa-spin");
    let response = await apiPost("Dashboard/GetItem3", data);
    toggleClassOff("item3spinner", "fa-spin");
    if (response.status === "OK") {
        let itemData = <Item3Type>response.data;
        (document.getElementById('i3A') as HTMLInputElement).value = itemData.a;
        (document.getElementById('i3B') as HTMLInputElement).value = itemData.b;
        (document.getElementById('i3Result') as HTMLInputElement).value = itemData.result;
        toggleClassOn("item3spinner", "w3-hide");
        toggleClassOff("item3container", "w3-hide");

    }
}

