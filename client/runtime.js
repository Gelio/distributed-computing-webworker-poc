
var Module = { 
	onRuntimeInitialized: function () {
		MONO.mono_load_runtime_and_bcl (
			"managed",
			"managed",
			0,
			[ "FactorialTask.dll","mscorlib.dll","bindings.dll" ],
			function () {
				Module.mono_bindings_init ("bindings");
				App.init ();
			});
	},
};
