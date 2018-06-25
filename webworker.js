const MAIN_URL = 'FactorialTask/bin/Debug/bridge/';
const requiredScriptNames = [
  'bridge.js',
  'bridge.console.js',
  'bridge.meta.js',
  'FactorialTask.js',
  'FactorialTask.meta.js'
];

try {
  const requiredScripts = requiredScriptNames.map(
    scriptName => MAIN_URL + scriptName
  );
  importScripts(...requiredScripts);
} catch (error) {
  console.error('Error while importing scripts', error);
}

self.onmessage = e => {
  const task = new FactorialTask.FactorialTask();
  const result = task.Perform(e.data);

  postMessage(result);
};
