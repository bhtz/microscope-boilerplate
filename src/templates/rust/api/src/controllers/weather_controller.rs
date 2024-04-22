use actix_web::{get, Responder, web};
use chrono::{Duration, Local, NaiveDate};
use rand::{Rng, thread_rng};
use serde::Serialize;
use utoipa::ToSchema;

#[derive(Debug, ToSchema, Serialize)]
pub struct WeatherForecast {
    date: NaiveDate,

    #[serde(rename = "temperatureC")]
    temperature_c: i32,

    summary: &'static str,

    #[serde(rename = "temperatureF")]
    temperature_f: i32,
}

impl WeatherForecast {
    pub fn new(date: NaiveDate, temperature_c: i32, summary: &'static str) -> WeatherForecast {
        let f = 32.0 + (temperature_c as f64 / 0.5556);

        WeatherForecast {
            date,
            temperature_c,
            summary,
            temperature_f: f as i32,
        }
    }
}

pub fn new(cfg: &mut web::ServiceConfig) {
    cfg.service(get_weather_forecasts);
}

#[utoipa::path(
    get,
    path = "/weatherforecast",
    responses(
        (status = 200, description = "anonymous", body = Vec<WeatherForecast>),
    )
)]
#[get("/weatherforecast")]
pub async fn get_weather_forecasts() -> impl Responder {
    let mut rng = thread_rng();
    let summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    let forecasts: Vec<WeatherForecast> = (1..=5).map(|index| {
        let date = Local::now().date_naive() + Duration::days(index as i64);
        let temperature_c = rng.gen_range(-20..=55);
        let summary = summaries[rng.gen_range(0..summaries.len())];

        return WeatherForecast::new(date, temperature_c, summary);
    }).collect();

    web::Json(forecasts)
}
