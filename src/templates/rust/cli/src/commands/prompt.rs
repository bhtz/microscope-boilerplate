use inquire::Text;

pub fn prompt_command(){
    let name = Text::new("Enter your name").prompt();

    match name {
        Ok(name) => println!("Hello {}", name),
        Err(_) => println!("An error happened when asking for your name, try again later."),
    }
}