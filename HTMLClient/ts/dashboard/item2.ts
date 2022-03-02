export {loadItem2}
async function loadItem2() {
    let container = <HTMLInputElement>document.getElementById('username');
    let data = {
        data:container.value
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
