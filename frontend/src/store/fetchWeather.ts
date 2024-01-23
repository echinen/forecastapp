import { createAsyncThunk } from "@reduxjs/toolkit";
import { ExtendedForecastData, WeatherData } from "../api/types";
import { fetchWeatherData } from "../api/weatherForecast";
import { getNextSevenDays } from "../utils/dateUtils";
import { setIsInitial, setIsLoading } from "./reducers/appReducer";

export const fetchWeather = createAsyncThunk(
  "weather/fetchWeather",
  async (address: string, { dispatch, rejectWithValue, fulfillWithValue }) => {
    dispatch(setIsLoading(true));

    try {
      const res = await fetchWeatherData(address);

      dispatch(setIsLoading(false));

      if (res.properties && res.properties.periods.length > 0) {
        dispatch(setIsInitial(false));
        return res;
      }
      return rejectWithValue("Error");
    } catch {
      dispatch(setIsLoading(false));
      return rejectWithValue("Error");
    }
  }
);

export const transformWeatherData = (
  res: any
): {
  weather: WeatherData;
  forecast: ExtendedForecastData[];
} => {
  var periods = res.properties.periods;
  const forecast: ExtendedForecastData[] = [];

  const weather: WeatherData = {
    weather: {
      description: periods[0].detailedForecast,
      icon: periods[0].icon,
      id: periods[0].number,
      main: periods[0].shortForecast,
    },
    main: {
      humidity: periods[0].relativeHumidity.value,
      pressure: 0,
      temp: periods[0].temperature,
      feels_like: periods[0].temperature,
      temp_max: periods[0].temperature,
      temp_min: periods[0].temperature,
    },
    wind: {
      speed: periods[0].windSpeed,
      deg: periods[0].dewpoint.value,
    },
    sys: {
      country: "",
      sunrise: 0,
      sunset: 0
    },
    name: periods[0].name,
  };

  // console.log('weather', weather)

  const next7Days = getNextSevenDays();
  periods.shift(); // remove current 
  periods.splice(-6); // get only the 7 first

  // console.log('periods', periods)

  periods.forEach((i: any, index: number) => {
    forecast.push({
      day: next7Days[index],
      temp: {
        temp_max: i.temperature,
        temp_min: i.temperature,
      },
      weather: {
        id: i.number,
        main: i.shortForecast,
        icon: i.icon,
      },
    });
  });

  return {
    weather,
    forecast,
  };
};
