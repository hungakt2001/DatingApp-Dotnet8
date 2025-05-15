import { Photo } from './Photo';

export interface Member {
  id: number;
  userName: string;
  age: number;
  photoUrl: string;
  dateOfBirth: string;
  knownAs: Date;
  created: Date;
  lastActive: string;
  gender: string;
  introduction: string;
  interests: string;
  lookingFor: any;
  country: string;
  city: string;
  photos: Photo[];
}
