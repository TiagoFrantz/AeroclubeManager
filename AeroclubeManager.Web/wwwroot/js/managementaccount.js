let inputCpf = document.getElementById("cpf")
let firstName = document.getElementById("firstNameInput")
let lastName = document.getElementById("lastNameInput")
let dataNascimento = document.getElementById("nascimento")
let submitButton = document.getElementById("salvarDados")

document.addEventListener("DOMContentLoaded", function () {
    const initialCpf = document.getElementById("cpf").value;
    const initialFirstName = document.getElementById("firstNameInput").value;
    const initialLastName = document.getElementById("lastNameInput").value;
    const initialDataNascimento = document.getElementById("nascimento").value;
    const initialSrc = document.getElementById("preview").value;


    function checkIfValuesChanged() {
        const currentCpf = document.getElementById("cpf").value;
        const currentFirstName = document.getElementById("firstNameInput").value;
        const currentLastName = document.getElementById("lastNameInput").value;
        const currentDataNascimento = document.getElementById("nascimento").value;
        const currentSrc = document.getElementById("preview");

        if (
            currentCpf === initialCpf &&
            currentFirstName === initialFirstName &&
            currentLastName === initialLastName &&
            currentDataNascimento === initialDataNascimento &&
            initialSrc === currentSrc
        ) {
            submitButton.disabled = true; // Desativa o botão
        } else {
            submitButton.disabled = false; // Ativa o botão
        }
    }

    document.getElementById("cpf").addEventListener("input", checkIfValuesChanged);
    document.getElementById("firstNameInput").addEventListener("input", checkIfValuesChanged);
    document.getElementById("lastNameInput").addEventListener("input", checkIfValuesChanged);
    document.getElementById("nascimento").addEventListener("input", checkIfValuesChanged);

    checkIfValuesChanged();
});


document.getElementById('cpf').addEventListener('input', function (e) {
    mascaraCpf()
});

function mascaraCpf() {
    var value = inputCpf.value;
    var cpfPattern = value.replace(/\D/g, '')
        .replace(/(\d{3})(\d)/, '$1.$2')
        .replace(/(\d{3})(\d)/, '$1.$2')
        .replace(/(\d{3})(\d)/, '$1-$2')
        .replace(/(-\d{2})\d+?$/, '$1');
    inputCpf.value = cpfPattern;
}
function validaCPF(cpf) {
    cpf = cpf.replace(/\D+/g, '');
    if (cpf.length !== 11) return false;

    let soma = 0;
    let resto;
    if (/^(\d)\1{10}$/.test(cpf)) return false; // Verifica sequências iguais

    for (let i = 1; i <= 9; i++) soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;
    if ((resto === 10) || (resto === 11)) resto = 0;
    if (resto !== parseInt(cpf.substring(9, 10))) return false;

    soma = 0;
    for (let i = 1; i <= 10; i++) soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);
    resto = (soma * 10) % 11;
    if ((resto === 10) || (resto === 11)) resto = 0;
    if (resto !== parseInt(cpf.substring(10, 11))) return false;

    return true;
}

mascaraCpf()



//funções da foto ->

let adicionarFotoButton = document.querySelector(".perfilImage__adicionar")
let perfilPopUp = document.querySelector(".perfilImagePopUp")
let dropContainer = document.getElementById("dropContainer")

let imagemSelecionada = true;


function mostrarPerfilPopUp() {
    perfilPopUp.classList.remove("close")

    perfilPopUp.classList.add("open")
}

function esconderPerfilPopUp() {
    perfilPopUp.classList.remove("open")
    perfilPopUp.classList.add("close")
}

document.addEventListener("click", function (event) {


    if (!dropContainer.contains(event.target) && !adicionarFotoButton.contains(event.target)) {
        esconderPerfilPopUp()
    }
})



let fileInput = document.getElementById("file");

dropContainer.ondragover = dropContainer.ondragenter = function (evt) {
    evt.preventDefault();
};

dropContainer.addEventListener('dragover', (event) => {
    event.preventDefault();
    dropContainer.classList.add('dragover');
});

dropContainer.addEventListener('mouseleave', () => {
    dropContainer.classList.remove('dragover');
});

dropContainer.addEventListener('mouseout', () => {
    dropContainer.classList.remove('dragover');
});


dropContainer.ondrop = function (evt) {
    // pretty simple -- but not for IE :(
    fileInput.files = evt.dataTransfer.files;

    // If you want to use some of the dropped files
    const dT = new DataTransfer();
    dT.items.add(evt.dataTransfer.files[0]);
    fileInput.files = dT.files;
    imagemSelecionada = true

    evt.preventDefault();
    updatePreview()
    dropContainer.classList.remove('dragover');

};

function removePerfilImage() {
    imagemSelecionada = false
    updatePreview()
}

function updatePreview() {

    const file = fileInput.files[0];

    if (file && imagemSelecionada) {
        const reader = new FileReader();

        reader.addEventListener('load', function () {
            previewImage.setAttribute('src', this.result);
            previewImage.style.display = 'block';
            esconderPerfilPopUp()
        });

        reader.readAsDataURL(file);

    } else {
        previewImage.setAttribute('src', "");
        esconderPerfilPopUp()

    }

}

const previewImage = document.getElementById('preview');

fileInput.addEventListener('change', function () {
    imagemSelecionada = true
    updatePreview()

});

