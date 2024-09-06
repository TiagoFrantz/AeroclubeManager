let nameInput = document.getElementById('name')
let descInput = document.getElementById('descricao')
let cidadeInput = document.getElementById('cidade')
let aeroportoInput = document.getElementById('aeroportoNome')
let estadosSelect = document.getElementById("estados")
let icaoInput = document.getElementById('icao')
let cnpjInput = document.getElementById('cnpj')
let emailInput = document.getElementById("email")
let ciacInput = document.getElementById("ciac")

let step1Button = document.getElementById('A_step1button')
let step2Button = document.getElementById('A_step2button')
let step3Button = document.getElementById('A_step3button')
let step4Button = document.getElementById('A_step4button')
let step5Button = document.getElementById('A_step5button')
let step6Button = document.getElementById('A_step6button')

let popUpLogoContainer = document.querySelector(".criar_logoPopUp");
let popUpLogoDrop = document.querySelector("#dropContainer");
let popUpOpenButton = document.querySelector(".perfilImage__adicionar");
let popUpRemoverButton = document.querySelector(".perfilImage__remover");

let imagemSelecionada = false;

async function buscarDistritos() {
    const cidade = document.getElementById('cidade').value;

    if (cidade.length < 3) return; // Espera até que o usuário digite pelo menos 3 caracteres

    const url = `https://servicodados.ibge.gov.br/api/v1/localidades/estados/${encodeURIComponent(estados.value)}/distritos
`;

    try {
        const response = await fetch(url);
        const distritos = await response.json();

        const listaDistritos = document.getElementById('distritos');
        listaDistritos.innerHTML = ''; // Limpa as opções anteriores

        distritos.forEach(distrito => {
            const option = document.createElement('option');
            option.value = distrito.nome;
            listaDistritos.appendChild(option);
        });
    } catch (error) {
        console.error('Erro ao buscar distritos:', error);
    }
}


function validarStep1() {
    if (nameInput.value.trim() == '') {
        return false
    }

    return true
}

function validarStep2() {
    if (descInput.value.trim() == '') {
        return false
    }

    return true
}

async function validarStep4() {

    if (cidadeInput.value.trim() != "") {
        const url = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/" + encodeURIComponent(estadosSelect.value) + "/distritos";

        try {
            const response = await fetch(url)
            const cidades = await response.json();
            console.log(cidades)

            const cidadesFiltradas = cidades.filter(cidade => cidade.nome.toLowerCase() == cidadeInput.value.toLowerCase());

            console.log("filc" + cidadesFiltradas)


            if (cidadesFiltradas.length <= 0) {
                Swal.fire({
                    title: 'Cidade não encontrada neste estado. Verfique o nome da cidade ou estado.',
                    icon: 'warning',
                })
                return false
            }

        } catch (error) {
            Swal.fire({
                title: 'Houve um erro ao tentar consultar a cidade.',
                icon: 'error',
            })
        }

    }


    if (cidadeInput.value.trim() == '') {
        Swal.fire({
            title: 'Coloque um valor válido na cidade',
            icon: 'warning',
        })
        return false
    } else if (aeroportoInput.value.trim() == '') {
        Swal.fire({
            title: 'Coloque um valor válido no nome do aeroporto',
            icon: 'warning',
        })
        return false
    } else if (icaoInput.value.trim() == '' || icaoInput.value.length != 4) {
        Swal.fire({
            title: 'Coloque um valor válido no ICAO',
            icon: 'warning',
        })
        return false
    }

    return true
}

function validarStep5() {
    if (!validarCNPJ(cnpjInput.value)) {
        Swal.fire({
            title: 'Coloque um valor válido no CNPJ',
            icon: 'warning',
        })
        return false
    }

    if (!validacaoEmail(emailInput)) {
        Swal.fire({
            title: 'Coloque um valor válido no e-mail',
            icon: 'warning',
        })
        return false
    }

    if (ciacInput.value.trim() == "") {
        Swal.fire({
            title: 'Coloque um valor válido no CIAC',
            icon: 'warning',
        })
        return false
    }

    return true
}

function updateLogoButtons() {
    if (imagemSelecionada) {
        popUpRemoverButton.style.display = "block";
        step3Button.textContent = "Avançar"
    } else {
        popUpRemoverButton.style.display = "none";
        step3Button.textContent = "Continuar s/ foto"
    }
}

updateLogoButtons()

function mostrarLogoPopUp() {
    popUpLogoContainer.classList.remove("close")
    popUpLogoContainer.classList.add("open");
    document.body.style.overflow = "hidden";
}

function fecharLogoPopUp() {
    popUpLogoContainer.classList.add("close")
    popUpLogoContainer.classList.remove("open");
    document.body.style.overflow = "auto";

}
document.addEventListener("click", function (event) {
    if (popUpLogoContainer.classList.contains("open")) {
        if (!popUpLogoDrop.contains(event.target) && !popUpOpenButton.contains(event.target)) {
            fecharLogoPopUp();
        }
    }
});



step1Button.addEventListener('click', () => {
    var valido = validarStep1()

    if (!valido) {
        Swal.fire({
            title: 'Coloque um valor válido no nome',
            icon: 'warning',
        })
        return
    }

    advanceStep(2)
})

step2Button.addEventListener('click', () => {
    let valido = validarStep2()

    if (!valido) {
        Swal.fire({
            title: 'Coloque um valor válido na descrição',
            icon: 'warning',
        })
        return
    }

    advanceStep(3)
})

step3Button.addEventListener('click', () => {
    advanceStep(4)
})

step4Button.addEventListener('click', async () => {
    let valido = await validarStep4()

    if (!valido) {
        return
    }

    advanceStep(5)
})


step5Button.addEventListener('click', () => {
    let valido = validarStep5()

    if (!valido) {
        return
    }

    advanceStep(6)
})

function previousStep(step) {
    let nextForm = document.getElementById('step' + step)

    if (nextForm != null) {
        nextForm.classList.add('open')
        nextForm.classList.remove('close')
        nextForm.classList.remove('initialClose')

    }
    let previousForm = document.getElementById('step' + (step + 1))

    if (previousForm != null) {
        previousForm.classList.remove('open')
        previousForm.classList.add('close')
    }

    //codigo
    console.log('stop ' + step)

    setValueInUrl(step)
    pressEnter(advanceStep, step + 1)
}

async function showStep(step) {
    var elements = document.querySelectorAll(
        '.criarAerocontainerC__content',
    )

    elements.forEach((div) => {
        div.classList.add('initialClose')
    })

    if (step > 6) {
        step = 1;
    }

    if (step > 5) {
        let valido6 = validarStep5();
        if (!valido6) {
            step = 5;
        }
    }

    if (step > 4) {
        let valido5 = await validarStep4();
        if (!valido5) {
            step = 4;
        }
    }

    if (step > 2) {
        let valido3 = validarStep2();
        if (!valido3) {
            step = 2;
        }
    }

    if (step > 1) {
        let valido2 = validarStep1();
        if (!valido2) {
            step = 1;
        }
    }



    var showDiv = document.querySelector('#step' + step)

    if (showDiv != null) {
        showDiv.classList.remove('initialClose')
        showDiv.classList.add('open')
        pressEnter(advanceStep, step + 1)

        return
    }

    showStep(1)
    pressEnter(advanceStep, 2)
}

function advanceStep(step) {
    let nextForm = document.getElementById('step' + step)

    if (nextForm != null) {
        setTimeout(function () {
            nextForm.classList.add('open')
            nextForm.classList.remove('close')
            nextForm.classList.remove('initialClose')
        }, 300)

        saveValuesInput()
        setValueInUrl(step)
    }

    let previousForm = document.getElementById('step' + (step - 1))
    console.log('previous ' + previousForm.id)

    if (previousForm != null) {
        previousForm.classList.remove('initialClose')
        previousForm.classList.add('close')
        previousForm.classList.remove('open')
    }

    //codigo
    console.log('stop ' + step)
    pressEnter(advanceStep, step + 1)
}




nameInput.addEventListener('input', saveValuesInput)
descInput.addEventListener('input', saveValuesInput)
cidadeInput.addEventListener('input', saveValuesInput)
estadosSelect.addEventListener('input', saveValuesInput)
aeroportoInput.addEventListener('input', saveValuesInput)
icaoInput.addEventListener('input', saveValuesInput)
cnpjInput.addEventListener('input', function (e) {
    var x = e.target.value.replace(/\D/g, '').match(/(\d{0,2})(\d{0,3})(\d{0,3})(\d{0,4})(\d{0,2})/);
    e.target.value = !x[2] ? x[1] : x[1] + '.' + x[2] + '.' + x[3] + '/' + x[4] + (x[5] ? '-' + x[5] : '');

    saveValuesInput()
});

function saveValuesInput() {
    sessionStorage.setItem('ca_name', nameInput.value)
    sessionStorage.setItem('ca_desc', descInput.value)
    sessionStorage.setItem('ca_cidade', cidadeInput.value)
    sessionStorage.setItem('ca_nomeAero', aeroportoInput.value)
    sessionStorage.setItem('ca_icao', icaoInput.value)
    sessionStorage.setItem('ca_cnpj', cnpjInput.value)
    sessionStorage.setItem('ca_email', emailInput.value)
    sessionStorage.setItem('ca_ciac', ciacInput.value)
    sessionStorage.setItem('ca_estado', estadosSelect.value)

}

function loadValuesInput() {
    nameInput.value = sessionStorage.getItem('ca_name')

    descInput.value = sessionStorage.getItem('ca_desc')

    cidadeInput.value = sessionStorage.getItem('ca_cidade')

    aeroportoInput.value = sessionStorage.getItem('ca_nomeAero')

    icaoInput.value = sessionStorage.getItem('ca_icao')

    cnpjInput.value = sessionStorage.getItem("ca_cnpj")

    emailInput.value = sessionStorage.getItem("ca_email")

    ciacInput.value = sessionStorage.getItem("ca_ciac")
    estadosSelect.value = sessionStorage.getItem("ca_estado")

}

function enterKeyListener(event) {
    if (event.key === 'Enter') {
        event.preventDefault()
    }
}

async function pressEnter(callbackFunction, parameter) {
    document.body.removeEventListener('keydown', enterKeyListener);

    enterKeyListener = async function (event) {
        if (event.key === 'Enter') {
            event.preventDefault();

            console.log('Parametro: ' + parameter);

            async function handleSwitch(parameter) {
                switch (parameter) {
                    // Caso 2
                    case 2:
                        let valido2 = validarStep1();
                        if (!valido2) {
                            Swal.fire({
                                title: 'Coloque um valor válido no nome',
                                icon: 'warning',
                            });
                            return false;
                        }
                        break;

                    case 3:
                        let valido3 = validarStep2();
                        if (!valido3) {
                            Swal.fire({
                                title: 'Coloque um valor válido na descrição',
                                icon: 'warning',
                            });
                            return false;
                        }
                        break;

                    case 5:
                        let valido5 = await validarStep4();
                        if (!valido5) {
                            return false;
                        }
                        break;

                    default:
                        return true;
                }
            }

            const result = await handleSwitch(parameter);
            if (result === false) {
                return;
            }

            callbackFunction(parameter);
        }
    };

    document.body.addEventListener('keydown', enterKeyListener);
}


loadValuesInput()

function getValueInUrl(param) {
    const urlParams = new URLSearchParams(window.location.search)
    return urlParams.get(param)
}

function setValueInUrl(param) {
    const url = new URL(window.location)
    url.searchParams.set('step', param)
    window.history.pushState({}, '', url)
}

document.addEventListener('DOMContentLoaded', () => {
    const urlParam = +getValueInUrl('step')

    showStep(urlParam)
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
            fecharLogoPopUp()
            updateLogoButtons()
        });

        reader.readAsDataURL(file);

    } else {
        previewImage.setAttribute('src', "");
        fecharLogoPopUp()
        updateLogoButtons()

    }

}


function validacaoEmail(field) {
    usuario = field.value.substring(0, field.value.indexOf("@"));
    dominio = field.value.substring(field.value.indexOf("@") + 1, field.value.length);

    if ((usuario.length >= 1) &&
        (dominio.length >= 3) &&
        (usuario.search("@") == -1) &&
        (dominio.search("@") == -1) &&
        (usuario.search(" ") == -1) &&
        (dominio.search(" ") == -1) &&
        (dominio.search(".") != -1) &&
        (dominio.indexOf(".") >= 1) &&
        (dominio.lastIndexOf(".") < dominio.length - 1)) {
        return true
    }
    else {
        return false
    }
}

function validarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;

}

const previewImage = document.getElementById('preview');

fileInput.addEventListener('change', function () {
    imagemSelecionada = true
    updatePreview()

});

async function criarAeroclube() {
    var valido = validarStep1() && validarStep2() && await validarStep4() && validarStep5();

    if (valido) {
        enviarDados()
    } else {
        Swal.fire({
            title: "Criação",
            text: "Não foi possível validar os dados do formulário. Por favor, revise eles.",
            icon: "error"
        });
    }

}


function mostrarMensagem(msg) {
    Swal.fire({
        title: msg,
        icon: 'info',
    })
}


function enviarDados() {
                            var formData = new FormData();

    formData.append('Nome', nameInput.value);
    formData.append('Descricao', descInput.value);
    formData.append('ImagemSelecionada', imagemSelecionada);
    formData.append('ImagemLogo', fileInput.files[0]);
    formData.append('Estado', estadosSelect.value);
    formData.append('Cidade', cidadeInput.value);
    formData.append('NomeAeroporto', aeroportoInput.value);
    formData.append('Icao', icaoInput.value);
    formData.append('Cnpj', cnpjInput.value);
    formData.append('Email', emailInput.value);
    formData.append('Ciac', ciacInput.value);


    $.ajax({
        url: postUrl,
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            Swal.fire({
                title: "Criação",
                text: response.message,
                icon: "success"
            });
        },
        error: function (xhr, status, error) {
            var errorMsg = xhr.responseJSON && xhr.responseJSON.message ? xhr.responseJSON.message : "Erro desconhecido"
            Swal.fire({
                title: "Criação",
                text: "Não foi possível atualizar os dados. Erro: " + errorMsg,
                icon: "error"
            });
        }
    });


}

step6Button.addEventListener("click", () => {
    criarAeroclube()
});