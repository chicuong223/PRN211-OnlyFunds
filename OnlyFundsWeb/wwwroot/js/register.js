

const firstname = document.getElementById('firstname');
const lastname = document.getElementById("lastname");
const username = document.getElementById("username");
const password = document.getElementById("password");
const confirmPassword = document.getElementById("confPass");
const email = document.getElementById("email");
const usernameRegex = /^[a-z0-9A-Z]{8,32}$/;
//const REGISTER_FORM = document.getElementById("register-btn");

//REGISTER_FORM.addEventListener(onsubmit, validateForm());
//
//function validateForm() {
//    let fname = document.getElementById("firstname");
//    let lname = document.getElementById("lastname");
//    let username = document.getElementById("username");
//    let password = document.getElementById("password");
//    let email = document.getElementById("email");
//    
//    

function validateName() {
    console.log(this.value);
    this.style.background = "yellowGreen";
    const NAME_REGEX = /^(^[a-zA-Z]+)$/; // does not allow names to contain numbers

    if (this.value.match(/^ *$/) !== null) {
        this.style.background = "pink";
        console.log("emp");
    }
    if (!NAME_REGEX.test(String(name.value))) {
        this.style.background = "pink";
        console.log("num");
    }
}

function validateInput(input, regex) {
    let valid = regex.test(input);
    return valid;
}
if (firstname) {
    firstname.addEventListener('blur', validateName);
}
//
if (firstname) {
    firstname.addEventListener('focus', function () {
        this.style.background = "white";
    });
}
//
if (lastname) {
    lastname.addEventListener('blur', validateName);
}
//
if (lastname) {
    lastname.addEventListener('focus', function () {
        this.style.background = "white";
    });
}

document.getElementById('register-form').addEventListener('submit', event => {
    const usernameRegex = /^([a-z0-9A-Z]+){8,32}$/;
    const nameRegex = /[a-zA-Z]/;
    const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    if (!validateInput(username.value, usernameRegex)
            || !validateInput(firstname.value, nameRegex)
            || !validateInput(lastname.value, nameRegex)
            || !validateInput(email.value, emailRegex)
            || password.value.length < 6
            || confirmPassword.value !== password.value) {
    }
    if (!validateInput(username.value, usernameRegex)) {
        event.preventDefault();
        console.log("username error");
        document.getElementById('usernameError').textContent = "Invalid username";
        return false;
    } else {
        document.getElementById('usernameError').textContent = "";
    }
    if (!validateInput(email.value, emailRegex)) {
        event.preventDefault();
        document.getElementById('emailError').textContent = "Invalid email";
        console.log("email error");
        return false;
    } else {
        document.getElementById('emailError').textContent = "";
    }
    if (password.value.length < 6) {
        console.log("password error");
        event.preventDefault();
        document.getElementById('passwordError').textContent = "Password length must be longer than 6 characters";
        return false;
    } else {
        document.getElementById('passwordError').textContent = "";
    }
    if (confirmPassword.value !== password.value) {
        console.log("confpass error");
        event.preventDefault();
        document.getElementById('confPassError').textContent = "Confirm password does not match";
    } else {
        document.getElementById('confPassError').textContent = "";
    }
    if(!validateInput(firstname.value, nameRegex) || !validationInput(lastname.value, nameRegex)){
        console.log("name error");
        event.preventDefault();
    }
});




