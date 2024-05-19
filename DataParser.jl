using CSV
using DataFrames

function split_semicolon_data(input_file::String, output_file::String)
    # Read the CSV file into a DataFrame
    df = CSV.read(input_file, DataFrame)
    
    split_rows = DataFrame()

    for row in eachrow(df)
        # Create a list of all columns in the row with semicolons
        columns_with_semicolons = [c for c in names(row) if occursin(";", row[c])]

        if isempty(columns_with_semicolons)
            push!(split_rows, row)
        else
            # Split the row into multiple rows based on the semicolon-separated values
            split_values = [row]
            for col in columns_with_semicolons
                new_split_values = DataFrame()
                for value_set in eachrow(split_values)
                    for split_value in split(value_set[col], ';')
                        new_row = copy(value_set)
                        new_row[col] = strip(split_value)
                        push!(new_split_values, new_row)
                    end
                end
                split_values = new_split_values
            end
            append!(split_rows, split_values)
        end
    end

    # Write the processed DataFrame to a new CSV file
    CSV.write(output_file, split_rows)
end

# Usage
input_file = "input.csv"
output_file = "output.csv"
split_semicolon_data(input_file, output_file)
