--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- Name: pracargarmenu(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION pracargarmenu(perfil text, OUT idmenu integer, OUT titulo text, OUT url character varying, OUT icono character varying, OUT idpadre integer) RETURNS SETOF record
    LANGUAGE plpgsql
    AS $$
BEGIN
RETURN QUERY SELECT m.idmenu, m.titulo, m.url, m.icono, m.idpadre FROM menu m JOIN perfildetalle p ON m.idmenu = p.idmenu WHERE p.idperfil = perfil;
END;
$$;


ALTER FUNCTION public.pracargarmenu(perfil text, OUT idmenu integer, OUT titulo text, OUT url character varying, OUT icono character varying, OUT idpadre integer) OWNER TO postgres;

--
-- Name: pralistarperfiles(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION pralistarperfiles(usuario text, OUT columna1 text, OUT columna2 text) RETURNS SETOF record
    LANGUAGE plpgsql
    AS $$
begin
return query select a.idperfil, a.nombre from perfil a inner join usuarioperfil
 b on a.idperfil=b.idperfil inner join usuarios c on b.idusuario = c.idusuario where c.iniciosesion = usuario;
 return;
end;
$$;


ALTER FUNCTION public.pralistarperfiles(usuario text, OUT columna1 text, OUT columna2 text) OWNER TO postgres;

--
-- Name: pravalidarclave(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION pravalidarclave(clave text) RETURNS SETOF boolean
    LANGUAGE plpgsql
    AS $$
	begin
	If ((select count (*) from usuarios as a where a.clave = clave) = 1) then
	return query select true;
	else
	return query select false;
	end if;
	end;
	$$;


ALTER FUNCTION public.pravalidarclave(clave text) OWNER TO postgres;

--
-- Name: praverificarperfil(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION praverificarperfil(usuario text) RETURNS SETOF integer
    LANGUAGE plpgsql
    AS $$
BEGIN
RETURN QUERY SELECT COUNT(*) FROM usuarioperfil up JOIN usuarios u ON up.idusuario = u.idusuario WHERE u.iniciosesion = usuario;
END;
$$;


ALTER FUNCTION public.praverificarperfil(usuario text) OWNER TO postgres;

--
-- Name: praverificarusuario(text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION praverificarusuario(usuario text) RETURNS SETOF boolean
    LANGUAGE plpgsql
    AS $$
begin
IF ((select count(*) from usuarios as a where a.iniciosesion = usuario) = 1) THEN 
return query select true;
else
return query select false;
END IF;
end;
$$;


ALTER FUNCTION public.praverificarusuario(usuario text) OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: menu; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE menu (
    idmenu integer NOT NULL,
    titulo text,
    url character varying(1000),
    icono character varying(1000),
    activo boolean,
    idpadre integer
);


ALTER TABLE public.menu OWNER TO postgres;

--
-- Name: menu_idmenu_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE menu_idmenu_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.menu_idmenu_seq OWNER TO postgres;

--
-- Name: menu_idmenu_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE menu_idmenu_seq OWNED BY menu.idmenu;


--
-- Name: perfil; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE perfil (
    idperfil text NOT NULL,
    nombre text,
    fechains date,
    fechaact date,
    activo boolean
);


ALTER TABLE public.perfil OWNER TO postgres;

--
-- Name: perfil_idperfil_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE perfil_idperfil_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.perfil_idperfil_seq OWNER TO postgres;

--
-- Name: perfil_idperfil_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE perfil_idperfil_seq OWNED BY perfil.idperfil;


--
-- Name: perfildetalle; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE perfildetalle (
    idperfil text NOT NULL,
    idmenu integer NOT NULL,
    activo boolean
);


ALTER TABLE public.perfildetalle OWNER TO postgres;

--
-- Name: usuarioperfil; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE usuarioperfil (
    idusuario integer NOT NULL,
    idperfil text NOT NULL,
    activo boolean
);


ALTER TABLE public.usuarioperfil OWNER TO postgres;

--
-- Name: usuarios; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE usuarios (
    idusuario integer NOT NULL,
    iniciosesion text,
    clave character varying(1000),
    nombrecompleto text,
    fechanamimiento date,
    activo boolean
);


ALTER TABLE public.usuarios OWNER TO postgres;

--
-- Name: usuarios_idusuario_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE usuarios_idusuario_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.usuarios_idusuario_seq OWNER TO postgres;

--
-- Name: usuarios_idusuario_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE usuarios_idusuario_seq OWNED BY usuarios.idusuario;


--
-- Name: idmenu; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY menu ALTER COLUMN idmenu SET DEFAULT nextval('menu_idmenu_seq'::regclass);


--
-- Name: idperfil; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY perfil ALTER COLUMN idperfil SET DEFAULT nextval('perfil_idperfil_seq'::regclass);


--
-- Name: idusuario; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY usuarios ALTER COLUMN idusuario SET DEFAULT nextval('usuarios_idusuario_seq'::regclass);


--
-- Data for Name: menu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY menu (idmenu, titulo, url, icono, activo, idpadre) FROM stdin;
2	Twitter	www.twitter.com	twitter.png	t	0
1	Facebook	https://www.facebook.com	fb.png	t	0
\.


--
-- Name: menu_idmenu_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('menu_idmenu_seq', 2, true);


--
-- Data for Name: perfil; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY perfil (idperfil, nombre, fechains, fechaact, activo) FROM stdin;
2	Usuario	2017-10-01	2017-10-01	f
1	Administrador	2017-09-21	2017-09-29	f
\.


--
-- Name: perfil_idperfil_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('perfil_idperfil_seq', 3, true);


--
-- Data for Name: perfildetalle; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY perfildetalle (idperfil, idmenu, activo) FROM stdin;
1	1	t
\.


--
-- Data for Name: usuarioperfil; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY usuarioperfil (idusuario, idperfil, activo) FROM stdin;
1	1	t
\.


--
-- Data for Name: usuarios; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY usuarios (idusuario, iniciosesion, clave, nombrecompleto, fechanamimiento, activo) FROM stdin;
4	bvillanueva	abc123	Beatriz Villanueva	2017-09-30	t
1	juanpineda	123	Juan Pineda	2017-09-21	t
5	msalume	asd123	Mario Salume	2017-10-01	t
6	smenjivar	abc123	Saul Menjivar	2017-10-01	t
\.


--
-- Name: usuarios_idusuario_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('usuarios_idusuario_seq', 6, true);


--
-- Name: menu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY menu
    ADD CONSTRAINT menu_pkey PRIMARY KEY (idmenu);


--
-- Name: perfil_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY perfil
    ADD CONSTRAINT perfil_pkey PRIMARY KEY (idperfil);


--
-- Name: perfildetalle_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY perfildetalle
    ADD CONSTRAINT perfildetalle_pkey PRIMARY KEY (idperfil, idmenu);


--
-- Name: usuarioperfil_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY usuarioperfil
    ADD CONSTRAINT usuarioperfil_pkey PRIMARY KEY (idusuario, idperfil);


--
-- Name: usuarios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (idusuario);


--
-- Name: perfildetalle__fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY perfildetalle
    ADD CONSTRAINT perfildetalle__fk FOREIGN KEY (idperfil) REFERENCES perfil(idperfil);


--
-- Name: perfildetalle__fk2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY perfildetalle
    ADD CONSTRAINT perfildetalle__fk2 FOREIGN KEY (idmenu) REFERENCES menu(idmenu);


--
-- Name: usuarioperfil_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY usuarioperfil
    ADD CONSTRAINT usuarioperfil_fk FOREIGN KEY (idusuario) REFERENCES usuarios(idusuario);


--
-- Name: usuarioperfil_fk2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY usuarioperfil
    ADD CONSTRAINT usuarioperfil_fk2 FOREIGN KEY (idperfil) REFERENCES perfil(idperfil);


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

