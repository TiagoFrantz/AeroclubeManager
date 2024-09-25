//Inputs -> 
const aeroName = document.getElementById("Name")
const aeroDesc = document.getElementById("inputDescricao")
const linkInput = document.getElementById("adicionarLink")
const textLinkInput = document.getElementById("adicionarTextoLink")
const cnpjInput = document.getElementById('cnpjInput')
const estadosSelect = document.getElementById("estadoAeroportoSelect")
const cidadeAeroInput = document.getElementById("localidadeAeroportoInput")
const logoInput = document.getElementById("InputLogo")
const icaoInput = document.getElementById("icaoInput")
const aeroportoNome = document.getElementById("nomeAeroportoInput")
const ciacInput = document.getElementById("ciacInput")
let links = [] // links do perfil 
var logoAlterado = false;
var linksAlterados = false;
let valoresIniciais = {}

document.addEventListener("DOMContentLoaded", () => {
    definirValoresIniciais()
})

function definirValoresIniciais() {
    valoresIniciais = {
        viAeroName: aeroName.value,
        viAeroDesc: aeroDesc.value,
        viCnpjInput: cnpjInput.value,
        viEstadosSelect: estadosSelect.value,
        viCidadeAero: cidadeAeroInput.value,
        viLogoInput: logoInput.value,
        viIcaoInput: icaoInput.value,
        viAeroportoNome: aeroportoNome.value,
        viCiacInput: ciacInput.value
    }
}

// validações ->

function validarNomeAeroclube() {
    if (aeroName.value.trim() == "") {
        return false
    }
    return true
}

function validarDescAeroclube() {
    if (aeroDesc.value.trim() == "") {
        return false
    }
    return true
}

function validarCnpjInput() {
    if (cnpjInput.value.trim() == "" || validarCNPJ(cnpjInput.value) == false) {
        return false
    }
    return true
}

async function validarCidadeAero() {
    if (cidadeAeroInput.value.trim() != "") {
        const url = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/" + encodeURIComponent(estadosSelect.value) + "/distritos";

        try {
            const response = await fetch(url)
            const cidades = await response.json();
            console.log(cidades)

            const cidadesFiltradas = cidades.filter(cidade => cidade.nome.toLowerCase() == cidadeAeroInput.value.toLowerCase());

            console.log("filc" + cidadesFiltradas)


            if (cidadesFiltradas.length <= 0) {
                return false
            }

        } catch (error) {
            Swal.fire({
                title: 'Houve um erro ao tentar consultar a cidade.',
                icon: 'error',
            })
        }

        return true
    }

    return false


}

function validarIcaoAero() {
    if (icaoInput.value.trim().length != 4) {
        return false
    }
    return true
}

function validarAeroportoNome() {
    if (aeroportoNome.value.trim() == "") {
        return false
    }
    return true
}

function validarCiac() {
    if (ciacInput.value.trim() == "") {
        return false
    }
    return true
}

function verificarValoresIniciasComAtuais() {
    if (valoresIniciais.viAeroName == aeroName.value &&
        valoresIniciais.viAeroDesc == aeroDesc.value &&
        valoresIniciais.viCnpjInput == cnpjInput.value &&
        valoresIniciais.viCidadeAero == cidadeAeroInput.value &&
        valoresIniciais.viEstadosSelect == estadosSelect.value &&
        valoresIniciais.viIcaoInput == icaoInput.value &&
        valoresIniciais.viAeroportoNome == aeroportoNome.value &&
        valoresIniciais.viCiacInput == ciacInput.value &&
        logoAlterado == false &&
        linksAlterados == false
    ) {
        return false
    }

    return true
}
// validações <-



var logoPreview = document.getElementById("logoPreview")


logoInput.addEventListener("change", function (event) {
    const file = event.target.files[0];
    console.log(file)
    if (file) {
        const reader = new FileReader();

        reader.onload = function (e) {
            logoPreview.src = e.target.result;
        }

        reader.readAsDataURL(file);

    }

    logoAlterado = true;
})







let linksList = document.querySelector(".aeroDados_content")

let linkButton = document.getElementById("buttonAdicionarLink")



let linkId = 0;

linkButton.addEventListener("click", () => {
    adicionarLink(textLinkInput.value, linkInput.value)
})

function adicionarLink(texto, link) {

    if (links.length >= 3) {
        Swal.fire({
            title: "Qnt. máxima de links",
            text: "A quantidade máxima de links é 3.",
            icon: 'info',
        })
        return
    }

    if (linkInput.value.trim() == "") {
        Swal.fire({
            title: "Coloque um valor válido no link",
            text: "O campo está vazio!",
            icon: 'info',
        })
        linkInput.focus()
        return
    }

    const newLink = { texto: texto, link: link, id: linkId }
    linkId++

    linkInput.value = "";
    textLinkInput.value = ""

    links.push(newLink)
    linksAlterados = true
    atualizarLista()
}

function removerLink(id) {
    const index = links.findIndex(link => link.id === id);

    if (index !== -1) {
        links.splice(index, 1);
        linksAlterados = true
    }

    atualizarLista()
}

function atualizarLista() {
    linksList.innerHTML = "";

    links.forEach(linkItem => {
        const contentItem = document.createElement("div")
        contentItem.classList.add("aeroDados_content_item")

        const span = document.createElement('span');
        span.innerHTML = linkItem.texto + " | <a target='_blank' href=" + linkItem.link + ">" + linkItem.link + "</a>";

        const button = document.createElement('button');
        button.addEventListener("click", () => {
            removerLink(linkItem.id)
        })
        button.classList.add('aeroDados_content_button');
        button.textContent = 'x';


        contentItem.appendChild(span);
        contentItem.appendChild(button);
        linksList.appendChild(contentItem);
    })

    if (links.length <= 0) {
        linksList.innerHTML = "<span style='color: white;'>Sem links</span>";
    }


}

atualizarLista()




const estados = [
    { sigla: "AC", nome: "Acre" },
    { sigla: "AL", nome: "Alagoas" },
    { sigla: "AP", nome: "Amapá" },
    { sigla: "AM", nome: "Amazonas" },
    { sigla: "BA", nome: "Bahia" },
    { sigla: "CE", nome: "Ceará" },
    { sigla: "DF", nome: "Distrito Federal" },
    { sigla: "ES", nome: "Espírito Santo" },
    { sigla: "GO", nome: "Goiás" },
    { sigla: "MA", nome: "Maranhão" },
    { sigla: "MT", nome: "Mato Grosso" },
    { sigla: "MS", nome: "Mato Grosso do Sul" },
    { sigla: "MG", nome: "Minas Gerais" },
    { sigla: "PA", nome: "Pará" },
    { sigla: "PB", nome: "Paraíba" },
    { sigla: "PR", nome: "Paraná" },
    { sigla: "PE", nome: "Pernambuco" },
    { sigla: "PI", nome: "Piauí" },
    { sigla: "RJ", nome: "Rio de Janeiro" },
    { sigla: "RN", nome: "Rio Grande do Norte" },
    { sigla: "RS", nome: "Rio Grande do Sul" },
    { sigla: "RO", nome: "Rondônia" },
    { sigla: "RR", nome: "Roraima" },
    { sigla: "SC", nome: "Santa Catarina" },
    { sigla: "SP", nome: "São Paulo" },
    { sigla: "SE", nome: "Sergipe" },
    { sigla: "TO", nome: "Tocantins" }
];


estados.forEach(estado => {
    const option = document.createElement("option");
    option.value = estado.sigla;
    option.text = estado.nome;
    estadosSelect.appendChild(option);
});


async function buscarDistritos() {
    const cidade = document.getElementById('localidadeAeroportoInput').value;

    if (cidade.length < 3) return;

    const url = `https://servicodados.ibge.gov.br/api/v1/localidades/estados/${encodeURIComponent(estadosSelect.value)}/distritos`;

    try {
        const response = await fetch(url);
        const distritos = await response.json();

        const listaDistritos = document.getElementById('distritos');
        listaDistritos.innerHTML = '';

        distritos.forEach(distrito => {
            const option = document.createElement('option');
            option.value = distrito.nome;
            listaDistritos.appendChild(option);
        });
    } catch (error) {
        console.error('Erro ao buscar distritos:', error);
    }
}




cnpjInput.addEventListener('input', function (e) {
    var x = e.target.value.replace(/\D/g, '').match(/(\d{0,2})(\d{0,3})(\d{0,3})(\d{0,4})(\d{0,2})/);
    e.target.value = !x[2] ? x[1] : x[1] + '.' + x[2] + '.' + x[3] + '/' + x[4] + (x[5] ? '-' + x[5] : '');

    saveValuesInput()
});



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