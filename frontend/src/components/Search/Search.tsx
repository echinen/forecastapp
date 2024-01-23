import React, { useEffect, useState } from 'react';
import { DebounceInput } from 'react-debounce-input';
import { useDispatch } from 'react-redux';
import { fetchWeather } from '../../store/fetchWeather';
import { LocationButton, LocationIcon, SearchElement, SearchIcon, SearchInput } from './styled';

const Search: React.FC = () => {
  const dispatch = useDispatch();
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    if (!searchTerm) {
      return;
    }
  }, [searchTerm]);

  const onSearchInputChanged = (e: any) => {
    setSearchTerm(e.target.value);
  };
  const sendLocation = (location: any) => {
    dispatch(fetchWeather(location));
  };
  return (
    <SearchElement>
      <SearchIcon />
      <DebounceInput element={SearchInput} debounceTimeout={300} onChange={onSearchInputChanged} placeholder="e.g. 538 Ramona St, Palo Alto, CA 94301, United States" />
      <LocationButton
        onClick={() => {
          if (searchTerm) {
            sendLocation(searchTerm);
          } else {
            alert('The address is missing, please fill in the search field.');
          }
        }}
      >
        <LocationIcon />
      </LocationButton>
    </SearchElement>
  );
};

export default Search;
