import csv

def split_semicolon_data(input_file, output_file):
    with open(input_file, 'r', newline='', encoding='utf-8') as infile:
        reader = csv.reader(infile)
        rows = list(reader)

    header = rows[0]
    data = rows[1:]

    split_data = []

    for row in data:
        # Find columns with semicolon and split them
        columns_with_semicolons = [i for i, value in enumerate(row) if ';' in value]
        if not columns_with_semicolons:
            split_data.append(row)
        else:
            # Get all possible combinations of the split values
            split_values = [row]
            for col in columns_with_semicolons:
                new_split_values = []
                for value_set in split_values:
                    for split_value in value_set[col].split(';'):
                        new_row = value_set.copy()
                        new_row[col] = split_value.strip()
                        new_split_values.append(new_row)
                split_values = new_split_values
            split_data.extend(split_values)

    # Write the processed data to a new CSV file
    with open(output_file, 'w', newline='', encoding='utf-8') as outfile:
        writer = csv.writer(outfile)
        writer.writerow(header)
        writer.writerows(split_data)

# Usage
input_file = "/content/FakeData.csv"
output_file = "/content/output.csv"
split_semicolon_data(input_file, output_file)
