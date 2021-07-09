//hidden field có tên là oldPass
const oldPass = document.getElementById('oldPass');
const oldPassin = document.getElementById('old-password');
const newPass = document.getElementById('new-password');
const confPass = document.getElementById('confPass');
const passRegex = /(?=.{6,})/;

let oldPassErr = document.getElementById('oldPassErr');
let newPassErr = document.getElementById('newPassErr');
let confPassErr = document.getElementById('confPassErr');

function validate(param) {
    if (param === 'oldPass') {
        console.log("Old password: " + oldPass);
        console.log("Input old password: " + oldPassin);

        if (oldPass.value !== oldPassin.value) {
            oldPassErr.innerHTML = '<i class="fas fa-times"></i> Old password is incorrect, try again';
            oldPassErr.style.display = "block";
            oldPassErr.style.color = "red";
            oldPassErr.className = "error";
        } else {
            oldPassErr.innerHTML = '<i class="fas fa-check"></i> Old password is correct';
            oldPassErr.style.display = "block";
            oldPassErr.style.color = "rgb(97 218 16)";
            oldPassErr.className = "success";
        }
    } else if (param === 'newPass') {
        if (passRegex.test(newPass.value) == false) {
            newPassErr.innerHTML = '<i class="fas fa-times"></i> New password is invalid, try again';
            newPassErr.style.display = "block";
            newPassErr.style.color = "red";
            newPassErr.className = "error";
        } else {
            newPassErr.innerHTML = '<i class="fas fa-check"></i> New password is valid';
            newPassErr.style.display = "block";
            newPassErr.style.color = "rgb(97 218 16)";
            newPassErr.className = "success";
        }
        if (confPass.value !== newPass.value) {
            confPassErr.innerHTML =
                '<i class="fas fa-times"></i> Password does not match'
            confPassErr.style.display = "block";
            confPassErr.className = "error";
            confPassErr.style.color = "red";
        } else {
            confPassErr.innerHTML =
                '<i class="fas fa-check"></i> Password matched'
            confPassErr.style.display = "block";
            confPassErr.className = "success";
            confPassErr.style.color = "rgb(97 218 16)";
        }
    } else if (param === 'confPass') {
        if (confPass.value !== newPass.value) {
            confPassErr.innerHTML =
                '<i class="fas fa-times"></i> Password does not match'
            confPassErr.style.display = "block";
            confPassErr.className = "error";
            confPassErr.style.color = "red";
        } else {
            confPassErr.innerHTML =
                '<i class="fas fa-check"></i> Password matched'
            confPassErr.style.display = "block";
            confPassErr.className = "success";
            confPassErr.style.color = "rgb(97 218 16)";
        }
    }
}

document.getElementById('report').addEventListener('submit', event => {
    const isError = document.querySelectorAll("span.error");
    console.log(isError);
    if (isError.length == 0) {
        alert("Succesfully registerd");
        return true;
    }
    else {
        alert('There is(are) invalid input(s), try again!');
        event.preventDefault();
    }
});