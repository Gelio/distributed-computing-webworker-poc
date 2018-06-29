const requiredScriptNames = [
  'runtime.js',
  'mono.js',
];

try {
  const requiredScripts = requiredScriptNames.map(
    scriptName => scriptName
  );
  importScripts(...requiredScripts);
} catch (error) {
  console.error('Error while importing scripts', error);
}

self.onmessage = e => {
  const performTask = Module.mono_bind_static_method("[FactorialTask] FactorialTask.FactorialTask:Perform");

  const result = performTask(e.data);

  postMessage(result);
};
