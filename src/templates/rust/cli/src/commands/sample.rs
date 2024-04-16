#[derive(clap::Args)]
#[command(author, version, about, long_about = None)]
pub struct SampleArgs {
    name: String
}

pub fn sample_command_handler(args: SampleArgs){
    println!("Hello {}", args.name)
}