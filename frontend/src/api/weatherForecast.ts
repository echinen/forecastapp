const baseUrl = 'http://localhost:5000';

export const fetchWeatherData = async (address: string) => {
    let url = `${baseUrl}/weatherforecast?address=${address}`;
  
    return await (await fetch(url)).json();
  };