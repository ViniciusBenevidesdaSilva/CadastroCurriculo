
// Form

// Mascara CPF
const cpfInput = document.getElementById('cpf');

cpfInput.addEventListener('keypress', (event) => {
    let inputlength = cpfInput.value.length;

    if (inputlength === 3 || inputlength === 7) {
        cpfInput.value += '.';
    } else if (inputlength === 11) {
        cpfInput.value += '-';
    }

    if (isNaN(event.key) || event.key == ' ') {
        event.preventDefault();
    }

})

// Marcara Telefone
const telefoneInput = document.getElementById('telefone');

telefoneInput.addEventListener('keypress', (event) => {
    let inputlength = telefoneInput.value.length;

    if (inputlength === 0) {
        telefoneInput.value += '(';
    } else if (inputlength === 3) {
        telefoneInput.value += ') ';
    }

    if (isNaN(event.key) || event.key == ' ') {
        event.preventDefault();
    }

})


// Mascara Salario

const salarioInput = document.getElementById('input-salario');

salarioInput.addEventListener('keypress', (event) => {

    if (isNaN(event.key) && event.key != ',') {
        event.preventDefault();
    }

    let invalidos = [' ', '/', '*', '-', '+'];

    if (invalidos.includes(event.key)) {
        event.preventDefault();
    }

    if (event.key === ',' && salarioInput.value.indexOf(',') !== -1) {
        event.preventDefault();
    }

})



// Alterar ícone a partir do icone Input
const idIconeInput = document.getElementById('Id_Icone');
const iconeImgForm = document.getElementById('iconeImgForm');

iconeImgForm.src = "/lib/img/icon" + idIconeInput.value + ".png";

idIconeInput.addEventListener('change', () => {
    iconeImgForm.src = "/lib/img/icon" + idIconeInput.value + ".png";
});




// Adicionar e remover experiências
const experienciasContainer = document.getElementById('experiencias-container');
const experienciasTemplate = document.getElementById('experiencias-template');


if (experienciasContainer.children.length === 1) {
    document.getElementById('no-experiencia').style.display = 'block'
}

function inserirExperiencia() {

    const clone = experienciasTemplate.cloneNode(true);
    const textarea = clone.querySelector('textarea');
    const inputHidden = clone.querySelector('input');

    textarea.setAttribute('name', `Experiencias[${experienciasContainer.children.length - 1}].Desc_Experiencia`);
    inputHidden.setAttribute('name', `Experiencias[${experienciasContainer.children.length - 1}].Id_Experiencia`);

    clone.style.display = 'block';

    clone.removeAttribute('id');
    textarea.removeAttribute('id');
    inputHidden.removeAttribute('id');

    experienciasContainer.appendChild(clone);
    document.getElementById('no-experiencia').style.display = 'none'

}


function removerExperiencia() {
    if (experienciasContainer.children.length > 1) {  // Mantem o template
        experienciasContainer.lastChild.remove();
    }

    if (experienciasContainer.children.length === 1) {
        document.getElementById('no-experiencia').style.display = 'block'
    }
}


const formacoesContainer = document.getElementById('formacoes-container');
const formacoesTemplate = document.getElementById('formacoes-template');

if (formacoesContainer.children.length === 1) {
    document.getElementById('no-formacao').style.display = 'block'
}

function inserirFormacao() {

    const clone = formacoesTemplate.cloneNode(true);
    const textarea = clone.querySelector('textarea');
    const inputHidden = clone.querySelector('input');

    textarea.setAttribute('name', `Formacoes[${formacoesContainer.children.length - 1}].Desc_Formacao`);
    inputHidden.setAttribute('name', `Formacoes[${formacoesContainer.children.length - 1}].Id_Formacao`);

    clone.style.display = 'block';

    clone.removeAttribute('id');
    textarea.removeAttribute('id');
    inputHidden.removeAttribute('id');

    formacoesContainer.appendChild(clone);
    document.getElementById('no-formacao').style.display = 'none'
}


function removerFormacao() {
    if (formacoesContainer.children.length > 1) {  // Mantem o template
        formacoesContainer.lastChild.remove();
    }
    if (formacoesContainer.children.length === 1) {
        document.getElementById('no-formacao').style.display = 'block'
    }
}


const idiomasContainer = document.getElementById('idiomas-container');
const idiomasTemplate = document.getElementById('idiomas-template');

if (idiomasContainer.children.length === 1) {
    document.getElementById('no-idioma').style.display = 'block'
}

function inserirIdioma() {
    const clone = idiomasTemplate.cloneNode(true);
    const input = clone.querySelector('#desc_idioma');
    const select = clone.querySelector('select');
    const inputHidden = clone.querySelector('#input-hidden');

    input.setAttribute('name', `Idiomas[${idiomasContainer.children.length - 1}].Desc_Idioma`);
    select.setAttribute('name', `Idiomas[${idiomasContainer.children.length - 1}].GrauProficiencia`);
    inputHidden.setAttribute('name', `Idiomas[${idiomasContainer.children.length - 1}].Id_Idioma`);

    clone.style.display = 'block';

    clone.removeAttribute('id');
    input.removeAttribute('id');
    select.removeAttribute('id');
    inputHidden.removeAttribute('id');

    // Preencher combobox
    const opcoesOriginais = idiomasTemplate.querySelector('select').querySelectorAll('option');  // Seleciona as opções originais
    const opcoesNovaCombo = select.querySelectorAll('option');

    for (let i = 0; i < opcoesOriginais.length; i++) {
        opcoesNovaCombo[i].value = opcoesOriginais[i].value;
        opcoesNovaCombo[i].textContent = opcoesOriginais[i].textContent;
    }

    idiomasContainer.appendChild(clone);
    document.getElementById('no-idioma').style.display = 'none'
}

function removerIdioma() {
    if (idiomasContainer.children.length > 1) {  // Mantem o template
        idiomasContainer.lastChild.remove();
    }
    if (idiomasContainer.children.length === 1) {
        document.getElementById('no-idioma').style.display = 'block'
    }
}


// End Form

