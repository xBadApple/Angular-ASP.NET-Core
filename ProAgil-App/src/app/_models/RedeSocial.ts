import { Evento } from './Evento';
import { Palestrante} from './Palestrante';

export interface RedeSocial {
    id: number;
    nomeId: string;
    urlId: string;
    eventoId?: number;
    evento: Evento;
    palestranteId?: number;
    palestrante: Palestrante;
}
