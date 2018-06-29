const MAIN_URL = '';
const requiredScriptNames = [
  'runtime.js',
  'mono.js',
];

let App = {
  init: () => { App.performTask = Module.mono_bind_static_method("[FactorialTask] FactorialTask.FactorialTask:Perform") }
}


try {
  const requiredScripts = requiredScriptNames.map(
    scriptName => MAIN_URL + scriptName
  );
  console.log('tests')
  importScripts(...requiredScripts);
} catch (error) {
  console.error('Error while importing scripts', error);
}

self.onmessage = e => {
  const result = App.performTask(e.data);

  postMessage(result);
};
