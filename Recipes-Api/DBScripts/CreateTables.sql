CREATE TABLE recipe (
	recipe_id SERIAL PRIMARY KEY,
	recipe_name varchar(64) NOT NULL
);

CREATE TABLE tool (
	tool_id SERIAL PRIMARY KEY,
	tool_name VARCHAR(64) NOT NULL
);

CREATE TABLE recipe_tool (
	recipe_id SERIAL REFERENCES recipe,
	tool_id SERIAL REFERENCES tool,
	quantity INTEGER NOT NULL,
	PRIMARY KEY(recipe_id, tool_id)
);

CREATE TABLE measurement (
	measurement_id SERIAL PRIMARY KEY,
	measurement_name VARCHAR(64) NOT NULL,
	measurement_symbol VARCHAR(8)
);

CREATE TABLE ingredient (
	ingredient_id SERIAL PRIMARY KEY,
	measurement_id SERIAL REFERENCES measurement,
	ingredient_name VARCHAR(64) NOT NULL
);

CREATE TABLE owned_ingredient (
	owned_ingredient_id SERIAL PRIMARY KEY,
	ingredient_id SERIAL REFERENCES measurement,
	quantity INTEGER NOT NULL
);

CREATE TABLE recipe_ingredient (
	recipe_id SERIAL REFERENCES recipe,
	ingredient_id SERIAL REFERENCES ingredient,
	quantity INTEGER NOT NULL,
	PRIMARY KEY(recipe_id, ingredient_id)
);


