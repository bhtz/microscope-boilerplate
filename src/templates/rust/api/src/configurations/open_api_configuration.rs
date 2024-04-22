use utoipa::OpenApi;
use utoipa_swagger_ui::SwaggerUi;
use crate::controllers::weather_controller;
use crate::controllers::weather_controller::WeatherForecast;

#[derive(OpenApi)]
#[openapi(
    paths(
        weather_controller::get_weather_forecasts,
    ),
    components(
        schemas(WeatherForecast)
    ),
    tags(
        (name = "microscope REST API", description = "microscope REST API")
    ),
)]
pub struct ApiDoc;

pub fn add_openapi_configuration() -> SwaggerUi {
    let openapi = ApiDoc::openapi();
    SwaggerUi::new("/swagger/{_:.*}").url("/api-docs/openapi.json", openapi.clone())
}
