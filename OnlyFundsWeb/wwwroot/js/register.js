let fname_error = document.getElementById('fnameError');
let lname_error = document.getElementById("lnameError");
let username_error = document.getElementById("userenameError");
let email_error = document.getElementById("emailError");
let pass_error = document.getElementById("passwordError");
let confPass_error = document.getElementById("confPassError");
const usernameRegex = /^[a-z0-9A-Z]{8,32}$/;
const nameRegex = /^[a-zA-Z]{1,}$/;
const passRegex = /(?=.{6,})/;
const emailRegex =
    /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;


function blockSpaceKey() {
    if (event.keyCode == 32) {
        return false;
    }
}

function validate(parameter) {
    if (parameter === 'username') {
        const uname = document.getElementById('username').value;
        if (usernameRegex.test(uname) == false) {
            username_error.innerHTML =
                '<i class="fas fa-times"></i> Username is not valid, try again';
            username_error.style.display = "block";
            username_error.className = "error";
            username_error.style.color = "red";
        } else {
            username_error.innerHTML =
                '<i class="fas fa-check"></i> Username is valid';
            username_error.style.display = "block";
            username_error.className = "success";
            username_error.style.color = "rgb(97 218 16)";
        }
    } else if (parameter === 'fname') {
        const fname = document.getElementById('firstname').value;
        if (nameRegex.test(fname) == false) {
            fname_error.innerHTML =
                '<i class="fas fa-times"></i> First name is not valid, try again'
            fname_error.style.display = "block";
            fname_error.className = "error";
            fname_error.style.color = "red";
        } else {
            fname_error.innerHTML =
                '<i class="fas fa-check"></i> First name is valid'
            fname_error.style.display = "block";
            fname_error.className = "success";
            fname_error.style.color = "rgb(97 218 16)";
        }
    } else if (parameter === 'lname') {
        const lname = document.getElementById('lastname').value;
        if (nameRegex.test(lname) == false) {
            lname_error.innerHTML =
                '<i class="fas fa-times"></i> Last name is not valid, try again'
            lname_error.style.display = "block";
            lname_error.className = "error";
            lname_error.style.color = "red";
        } else {
            lname_error.innerHTML =
                '<i class="fas fa-check"></i> Last name is valid'
            lname_error.style.display = "block";
            lname_error.className = "success";
            lname_error.style.color = "rgb(97 218 16)";
        }
    } else if (parameter === 'email') {
        const email = document.getElementById('email').value;
        if (emailRegex.test(email) == false) {
            email_error.innerHTML =
                '<i class="fas fa-times"></i> Email is not valid, try again'
            email_error.style.display = "block";
            email_error.className = "error";
            email_error.style.color = "red";
        } else {
            email_error.innerHTML =
                '<i class="fas fa-check"></i> Email is valid'
            email_error.style.display = "block";
            email_error.className = "success";
            email_error.style.color = "rgb(97 218 16)";
        }
    } else if (parameter === 'password') {
        const password = document.getElementById('password').value;
        const pass = document.getElementById('password').value;
        const confPass = document.getElementById('confPass').value;
        if (passRegex.test(password) == false) {
            pass_error.innerHTML =
                '<i class="fas fa-times"></i> Password is not valid, try again'
            pass_error.style.display = "block";
            pass_error.className = "error";
            pass_error.style.color = "red";
        } else {
            pass_error.innerHTML =
                '<i class="fas fa-check"></i> Password is valid'
            pass_error.style.display = "block";
            pass_error.className = "success";
            pass_error.style.color = "rgb(97 218 16)";
        }
        if (confPass !== pass) {
            confPass_error.innerHTML =
                '<i class="fas fa-times"></i> Password does not match'
            confPass_error.style.display = "block";
            confPass_error.className = "error";
            confPass_error.style.color = "red";
        } else {
            confPass_error.innerHTML =
                '<i class="fas fa-check"></i> Password matched'
            confPass_error.style.display = "block";
            confPass_error.className = "success";
            confPass_error.style.color = "rgb(97 218 16)";
        }
    } else if (parameter === 'confPass') {
        const pass = document.getElementById('password').value;
        const confPass = document.getElementById('confPass').value;
        if (confPass !== pass) {
            confPass_error.innerHTML =
                '<i class="fas fa-times"></i> Password does not match'
            confPass_error.style.display = "block";
            confPass_error.className = "error";
            confPass_error.style.color = "red";
        } else {
            confPass_error.innerHTML =
                '<i class="fas fa-check"></i> Password matched'
            confPass_error.style.display = "block";
            confPass_error.className = "success";
            confPass_error.style.color = "rgb(97 218 16)";
        }
    }
}

document.getElementById('reg-form').addEventListener('submit', e => {
    const isError = document.querySelectorAll("span.error");
    console.log(isError);
    if (isError.length == 0) {
        alert("Succesfully registerd");
    }
    else {
        alert('There is(are) invalid input(s), try again!');
        e.preventDefault();
        return false;
    }
});





