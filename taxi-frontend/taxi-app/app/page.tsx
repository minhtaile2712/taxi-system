"use client";

import React, { useState, useEffect } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import useGeolocation from "../hooks/useGeolocation";
import { useRouter, useSearchParams } from "next/navigation";
import { updateLocation } from "../services/customers";
import { postBookings } from "@/services/booking";

interface FormValues {
  name: string;
  choose: string;
}

export default function Home() {
  const { location, error } = useGeolocation();
  const router = useRouter();
  const searchParams = useSearchParams();
  const id = searchParams.get("id");

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>();

  useEffect(() => {
    if (!id) {
      router.push("/login");
    }
  }, [id, router]);

  useEffect(() => {
    const updateLocationData = async () => {
      if (location && id) {
        const data = {
          long: location.longitude,
          lat: location.latitude,
        };
        try {
          await updateLocation(id, data);
        } catch (error) {
          console.error("Failed to update location:", error);
        }
      }
    };

    updateLocationData();
  }, [id, location]);

  const onSubmit: SubmitHandler<FormValues> = async (data) => {
    try {
      if (data.name && id) {
        const coords: string[] = data.name
          .split(",")
          .map((coord) => coord.trim());
        if (coords.length === 2) {
          const [latStr, longStr] = coords;
          const latitude: number = parseFloat(latStr);
          const longitude: number = parseFloat(longStr);
          const data = {
            long: longitude,
            lat: latitude,
          };
          await updateLocation(id, data);
        } else {
          console.error(
            "Invalid coordinates format. Expected 'latitude, longitude'"
          );
        }
        await postBookings({ customerId: id });
        router.push("/bookingSuccess");
      }
    } catch (error) {
      console.error("Failed to post booking:", error);
    }
  };

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-center font-bold text-2xl mb-4">Home</h1>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <div>
          <label
            htmlFor="name"
            className="block text-sm font-medium text-gray-700"
          >
            Name
          </label>
          <input
            id="name"
            type="text"
            {...register("name", { required: true })}
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
          />
          {errors.name && (
            <span className="text-red-500 text-sm">This field is required</span>
          )}
        </div>
        <div className="flex space-x-4">
          <button
            type="submit"
            className="inline-flex justify-center rounded-md border border-transparent bg-indigo-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-indigo-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
          >
            Submit
          </button>
          <button
            type="button"
            onClick={() => {
              /* Handle the other button action here */
            }}
            className="inline-flex justify-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
}
