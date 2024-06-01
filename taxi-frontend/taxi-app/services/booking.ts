import { BookingMakeDto } from '@/dtos/Customer.dto';
import { apiClient } from './apiClient';


  //bookings driver
    export const postBookings = async (data :BookingMakeDto) => {
        try {
        const response = await apiClient.post('/Bookings',data);
        return response.data;
        } catch (error) {
        console.error('Failed to fetch bookings:', error);
        throw error;
        }
    };
