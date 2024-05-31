import axios from 'axios';
import { DriverDTO, LocationDTO } from '../dtos/Driver.dto';

const apiClient = axios.create({
    baseURL: 'https://localhost:7283/api',
    headers: {
      'Content-Type': 'application/json',
    },
  });
//get drivers
  export const getDrivers = async () => {
    try {
      const response = await apiClient.get('/Drivers');
      return response.data;
    } catch (error) {
      console.error('Failed to fetch drivers:', error);
      throw error;
    }
  };
//create driver
export const addDriver = async (driver: DriverDTO): Promise<DriverDTO> => {
    try {
      const response = await apiClient.post('/Drivers', driver);
      return response.data;
    } catch (error) {
      console.error('Failed to add driver:', error);
      throw error;
    }
  };
  //get driver near by
    export const getNearByDrivers = async (Long : number, Lat  : number,DistanceInMeters:number) => {
        try {
        const response = await apiClient.get(`/Drivers/nearby?Long =${Long }&Lat =${Lat}&DistanceInMeters =${DistanceInMeters}`);
        return response.data;
        } catch (error) {
        console.error('Failed to fetch nearby drivers:', error);
        throw error;
        }
    };
    //get driver by id
    export const getDriverById = async (id: number) => {
        try {
        const response = await apiClient.get(`/Drivers/${id}`);
        return response.data;
        } catch (error) {
        console.error('Failed to fetch driver:', error);
        throw error;
        }
    };
    //delete driver
    export const deleteDriver = async (id: number) => {
        try {
        const response = await apiClient.delete(`/Drivers/${id}`);
        return response.data;
        } catch (error) {
        console.error('Failed to delete driver:', error);
        throw error;
        }
    };
    //update driver location
    export const updateLocation = async (userId: number, location: LocationDTO): Promise<void> => {
        try {
            const response =await apiClient.post(`/Users/${userId}/location`, location);
            return response.data;
        } catch (error) {
          console.error('Failed to update location:', error);
          throw error;
        }
      };