const worker = new Worker('./webworker.js');
const dataForm = document.getElementById('data-form');
const inputData = document.getElementById('input-data');
const submitButton = document.getElementById('submit-button');
const resultSpan = document.getElementById('result');

let taskQueued = false;

dataForm.onsubmit = e => {
  e.preventDefault();
  if (taskQueued) {
    return;
  }

  taskQueued = true;

  worker.postMessage(inputData.value);
};

worker.onmessage = e => {
  taskQueued = false;
  resultSpan.innerText = e.data;
};

worker.onerror = e => {
  taskQueued = false;
  resultSpan.innerText = 'error (see the console)';

  console.error('Error from worker', e);
};
