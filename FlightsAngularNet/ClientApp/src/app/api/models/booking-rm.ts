/* tslint:disable */
/* eslint-disable */
import { TimePlaceRm } from './time-place-rm';
export interface BookingRm {
  airline?: null | string;
  arrival?: TimePlaceRm;
  departure?: TimePlaceRm;
  flighItd?: string;
  numberOfBookedSeats?: number;
  passengerEmail?: null | string;
  price?: null | string;
}
