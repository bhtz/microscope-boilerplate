mod commands;

use std::env::args;
use clap::{Parser};
use figlet_rs::FIGfont;
use commands::sample::{SampleArgs, sample_command_handler};
use commands::prompt::{ prompt_command };

#[derive(Parser)]
#[command(name = "mcsp-rs")]
#[command(bin_name = "mcsp-rs")]
enum CliCommand {
    #[command(name = "sample")]
    Sample(SampleArgs),

    #[command(name = "prompt")]
    Prompt,
}

fn main() {
    if args().len() <= 1 {
        figlet("Microscope")
    }
    else {
        let cli = CliCommand::parse();
        match cli {
            CliCommand::Sample(args) => sample_command_handler(args),
            CliCommand::Prompt => prompt_command()
        }
    }
}

fn figlet(figlet: &str){
    let standard_font = FIGfont::standard().unwrap();
    let figure = standard_font.convert(figlet);
    println!("{}", figure.unwrap());
}
