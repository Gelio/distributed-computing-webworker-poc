const requiredScriptNames = ['runtime.js', 'mono.js'];

var App = {
  init() {
    console.log('Worker initialized');
    onAppInit();
  }
};

try {
  importScripts(...requiredScriptNames);
} catch (error) {
  console.error('Error while importing scripts', error);
}

function onAppInit() {
  // NOTE: This needs to run after the worker is initialized
  BINDING.bindings_lazy_init();

  const config = {
    assemblyName: 'FactorialTask',
    namespace: 'FactorialTask',
    className: 'FactorialTask',
    performMethodName: 'Perform'
  };

  const loadedAssembly = BINDING.assembly_load(config.assemblyName);

  const taskClass = BINDING.find_class(
    loadedAssembly,
    config.namespace,
    config.className
  );

  // No idea what -1 means, but it is used in all find_method calls
  const performMethod = BINDING.find_method(
    taskClass,
    config.performMethodName,
    -1
  );

  const createTaskInstance = Module.mono_bind_static_method(
    '[Common] Common.TaskCreator:CreateTask'
  );

  const taskInstanceJSObj = createTaskInstance(
    config.assemblyName,
    `${config.namespace}.${config.className}`
  );
  const taskInstance = BINDING.extract_mono_obj(taskInstanceJSObj);

  self.onmessage = e => {
    const argument = e.data;
    const result = BINDING.call_method(performMethod, taskInstance, 's', [
      argument
    ]);

    postMessage(result);
  };
}
