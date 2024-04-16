import { input } from '@inquirer/prompts';

// prompt command with inquirer
export default async function promptCommand() {
    const name = await input({ message: 'Enter your name' });
    console.log(`Hello ${name} !`);
}
