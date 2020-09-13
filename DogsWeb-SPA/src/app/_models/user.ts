import { Photo } from './photo';

export interface User {
      id: string;
      username: string;
      emailConfirmed: number;
      knownAs?: string;
      age: number;
      breed: string;
      created: Date;
      lastActive: Date;
      city: string;
      interests?: string;
      introduction?: string;
      lookingFor?: string;
      photoUrl?: string;
      photos?: Photo[];
}
