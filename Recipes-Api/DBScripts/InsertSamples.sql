-- RECIPES
INSERT INTO recipe (recipe_name) VALUES ('Cereals');

-- TOOLS
INSERT INTO tool (tool_name) VALUES ('Spoon');
INSERT INTO tool (tool_name) VALUES ('Bowl');

-- RECIPE_TOOLS
INSERT INTO recipe_tool (recipe_id, tool_id, quantity) VALUES (1, 1, 1);

-- MEASUREMENTS
INSERT INTO measurement (measurement_name, measurement_symbol) VALUES ('Gram', 'g');
INSERT INTO measurement (measurement_name, measurement_symbol) VALUES ('Liter', 'L');
INSERT INTO measurement (measurement_name, measurement_symbol) VALUES ('Count', '');

-- INGREDIENTS
INSERT INTO ingredient (measurement_id, ingredient_name) VALUES (1, 'Cereal');
INSERT INTO ingredient (measurement_id, ingredient_name) VALUES (2, 'Milk');

-- RECIPE_INGREDIENT
INSERT INTO recipe_ingredient_measurement (recipe_id, ingredient_id, measurement_id, quantity) VALUES (1, 1, 300);
INSERT INTO recipe_ingredient_measurement (recipe_id, ingredient_id, measurement_id, quantity) VALUES (1, 2, 0.25);



