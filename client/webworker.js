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

  /**
   * Alternatively, resolve_method_fqn may be used:
   *
   * ```ts
   * const performMethod = BINDING.resolve_method_fqn('[FactorialTask] FactorialTask.FactorialTask:Perform');
   * ```
   */

  // NOTE: The argument is arbitrary.
  const taskInstance = BINDING.wasm_binding_obj_new(1234);

  self.onmessage = e => {
    const argument = e.data;
    const result = BINDING.call_method(performMethod, taskInstance, 's', [
      argument
    ]);

    postMessage(result);
  };
}
