# Distributed computing in JS using C# - proof of concept

The idea is to write all the code in C#, split the solution into multiple projects - one of them
will be compiled into WebAssembly and will be distributed, the other one will be included as a DLL on the
server.

The project consists of the following parts

1.  C# solution
2.  JS client

They will be described below. The library used to compile C# into WebAssembly is
[mono](https://github.com/mono/mono).

## C# solution

The C# solution contains 4 projects:

- `Common` - common interfaces that should be the base of the library produced as a result of our
  thesis.

  - `IDistributedTask` - an interface that should be implemented by a class that will be run on the
    server. It should be able to split the input data and aggregate the results.
  - `ITaskFactory` - an interface provided to `IDistributedTask` that is able to run new clients
    when `IDistributedTask` splits the input data.
  - `ITask` - an interface that should be implemented by a class that will be run on the client in
    the browser. It should be able to compute the final result given some input data.

- `FactorialSumDistributed` - the main task definition. The idea is to split the input data and then
  compute a factorial for each part, summing the results.

- `FactorialTask` - the code that calculates the factorial. It will be compiled to WebAssembly via mono
  and will be run in the browser in a WebWorker.

  Currently all the files from the `Common` project are added in the `FactorialTask` as links.

- `ServerRunner` - a console application that loads DLL assemblies for `FactorialSumDistributed` and
  `FactorialTask`, asks for data and simulates the distributed computation.

## JS client

The JS client loads WebAssembly code into a WebWorker and sends any input data from the form to the
worker, displaying the results as they arrive.

### Compile code to WebAssembly

```shell
./build.sh
```

Client cannot be run directly from the filesystem due to how WebWorkers work. It has to be hosted using
a server.

Python's `SimpleHTTPServer`:

```bash
python -m SimpleHTTPServer 8000
```

or Node's `http-server` may be used:

```bash
npx http-server -c-1
```

The `client/webworker.html` file should be opened.
