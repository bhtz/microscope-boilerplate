mod commands;

use std::env::args;
use std::io;
use clap::{Parser};
use figlet_rs::FIGfont;
use commands::sample::{SampleArgs, sample_command_handler};

#[derive(Parser)]
#[command(name = "mcsp-rs")]
#[command(bin_name = "mcsp-rs")]
enum CliCommand {
    #[command(name = "sample")]
    Sample(SampleArgs),
}

fn main() {
    figlet("MICROSCOPE");

    if args().len() <= 1 {
        default_handler()
    }
    else {
        let cli = CliCommand::parse();

        match cli {
            CliCommand::Sample(args) => sample_command_handler(args),
        }
    }
}

fn default_handler() {
    let mut input = String::new();

    io::stdin()
        .read_line(&mut input)
        .expect("Failed to read line");
}

fn figlet(figlet: &str){
    let standard_font = FIGfont::standard().unwrap();
    let figure = standard_font.convert(figlet);
    println!("{}", figure.unwrap());
}
