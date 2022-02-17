function initLogin() {
    document.addEventListener("DOMContentLoaded", () => {
        if (stateservice != null) {
        }
        else {
        }
    });
    document.getElementById("login").addEventListener("click", async () => {
        let data = {
            username: document.getElementById('username').value,
            password: document.getElementById('password').value
        };
        let response = await apiPost("Account/Login", data);
        if (response.status === "OK") {
            if (response.data.user !== null) {
                stateservice = response.data;
                localStorage.setItem('stateservice', JSON.stringify(stateservice));
                window.location.href = '../';
            }
        }
    });
}
//# sourceMappingURL=login.js.map