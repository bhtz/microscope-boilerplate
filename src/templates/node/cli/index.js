import figlet from 'figlet';
import { program } from 'commander';
import sampleCommand from './commands/sample.command.js';
import promptCommand from './commands/prompt.command.js';

// Set the version of your CLI tool
program.version('1.0.0');

// Define your default command
program.action(() => {
    figlet("Microscope", (err, data) => {
        if (err) {
            console.log("Something went wrong...");
            console.dir(err);
            return;
        }
        console.log(data);
    });
});

// add a command like this
program
    .command('sample [name]') // optional params instead of <param> syntax
    .description('Run the sample command')
    .action(sampleCommand);

    program
    .command('prompt') // optional params instead of <param> syntax
    .description('Run the prompt command')
    .action(promptCommand);

// Parse the command-line arguments
program.parse(process.argv);
