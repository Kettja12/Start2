export { initUserInformation}
import { savePassword, saveUser, getMessage } from './accountapi.js'
let userInformationTranslations = {
    'Failed to fetch': 'Database connection failed.',
    'Password and password verification are not same.': 'Password and password verification are not same.',
    'User save success.': 'User save success.'
}

async function initUserInformation(e) {

    document.getElementById("saveUser").addEventListener("click", async () => saveuserInformation());
    let content = document.getElementById("divUserInformation");

    let xpos = e.pageX - 300;
    (document.getElementById('username') as HTMLInputElement).value = stateservice.user.username;
    (document.getElementById('lastname') as HTMLInputElement).value = stateservice.user.lastName;
    (document.getElementById('firstname') as HTMLInputElement).value = stateservice.user.firstName;
    (document.getElementById('isadmin') as HTMLInputElement).checked = isAdmin(stateservice.user.claims);
    content.style.position = 'absolute';
    content.style.left = xpos + "px";
    content.classList.toggle("w3-hide");

}

async function saveuserInformation() {
    let c = document.getElementById("userInformationTranslations");
    userInformationTranslations = JSON.parse(c.innerHTML);
    let oldpassword = (document.getElementById('oldpassword') as HTMLInputElement).value
    let newpassword = (document.getElementById('newpassword') as HTMLInputElement).value
    let passwordverification = (document.getElementById('passwordverification') as HTMLInputElement).value

    let e = document.getElementById('infomessage');
    if (newpassword != passwordverification) {
        showError(e, translate(userInformationTranslations, "Password and password verification are not same."));
        return ;
    }
    stateservice.user.firstName = (document.getElementById('firstname') as HTMLInputElement).value
    stateservice.user.lastName = (document.getElementById('lastname') as HTMLInputElement).value

    //let response = await apiPost("account/saveuser", stateservice.user);
    let response = saveUser(stateservice.user);
    localStorage.setItem('stateservice', JSON.stringify(stateservice));
    if (response!== null) {
    //if (response.status === "OK") {
        let m = translate(userInformationTranslations, "User save success.");
        if (newpassword !== '') {
            let data: SavePasswordModel = {
                newPassword: newpassword,
                oldPassword: oldpassword,
                username: stateservice.user.username
            }
            //response = await apiPost("account/savePassword", data);
            let response = savePassword(data);
                m = m + ' ' + translate(userInformationTranslations, response);
         
        }
        let content = document.getElementById("divUserInformation");
        content.classList.toggle("w3-hide");
        showError(e, m);

    }
    else {
        showError(e, translate(userInformationTranslations, getMessage()));
    }

}
