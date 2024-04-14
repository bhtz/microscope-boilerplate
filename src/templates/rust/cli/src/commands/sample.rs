#[derive(clap::Args)]
#[command(author, version, about, long_about = None)]
pub struct SampleArgs {
    package_name: String,
    max_depth: usize,
}

pub fn sample_command_handler(args: SampleArgs){
    println!("{:?} -- {}", args.package_name, args.max_depth)
}