import { apiClient } from './apiClient';
import { LocationDTO } from '../dtos/Driver.dto';


//get all customers
    export const getCustomers = async () => {
        try {
          const response = await apiClient.get('/Customers');
          return response.data;
        } catch (error) {
          console.error('Failed to fetch customers:', error);
          throw error;
        }
      };

    //get customer by id
    export const getCustomerById = async (id: number) => {
        try {
        const response = await apiClient.get(`/Customers/${id}`);
        return response.data;
        } catch (error) {
        console.error('Failed to fetch customer:', error);
        throw error;
        }
    };
    //delete customer
    export const deleteCustomer = async (id: number) => {
        try {
        const response = await apiClient.delete(`/Customers/${id}`);
        return response.data;
        } catch (error) {
        console.error('Failed to delete customer:', error);
        throw error;
        }
    };
    //update customer location
    export const updateLocation = async (userId: string, location: LocationDTO): Promise<void> => {
        try {
            const response =await apiClient.post(`/Customers/${userId}/location`, location);
            return response.data;
        } catch (error) {
            console.error('Failed to update customer location:', error);
            throw error;
        }
    };
    //add customer
    export const addCustomer = async (phoneNumber: string): Promise<any> => {
      try {
          console.log(phoneNumber);
        const response = await apiClient.post('/Customers', { phoneNumber });
        return response.data;
      } catch (error) {
        console.error('Failed to add customer:', error);
        throw error;
      }
    };