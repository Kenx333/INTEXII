import pandas as pd
import numpy as np
from sklearn.tree import DecisionTreeClassifier # Import Decision Tree Classifier
from sklearn.model_selection import train_test_split # Import train_test_split function
from sklearn import metrics #Import scikit-learn metrics module for accuracy calculation

df = pd.read_csv("C:\Users\kinse\Downloads\modified_order.csv")
df.head()

# Ensure the date column is in datetime format
df['date'] = pd.to_datetime(df['date'])

# Create new columns based on the date column
df['hour_of_day'] = df['date'].dt.hour
df['day_of_week'] = df['date'].dt.day_name()  # This will be automatically recognized as categorical
df['day_of_month'] = df['date'].dt.day
df['month_of_year'] = df['date'].dt.month
df['year'] = df['date'].dt.year

# Drop the original date column
df = df.drop(columns=['date'])

def Xandy(df, label):
  import pandas as pd
  y = df[label]
  X = df.drop(columns=[label])
  return X, y

def dummy_code(X):
  import pandas as pd
  X = pd.get_dummies(X, drop_first=True)
  return X

X, y = Xandy(df, 'fraud')
X = dummy_code(X)

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.3, random_state=1) # 70% training and 30% test

model = DecisionTreeClassifier(max_depth=5).fit(X_train, y_train)

print(f'Accuracy:\t{model.score(X_test, y_test)}')
print(y_test.value_counts() / y_test.shape[0])

# # TESTING
# # Example input features
# hair, feathers, eggs, milk, airborne, aquatic, predator, toothed, backbone, breathes, venomous, fins, legs, tail, domestic, catsize
# input_features = [0,1,1,0,1,0,0,0,0,1,0,0,2,0,0,0] 
# print(model.predict(input_features))


# Converting the model to ONNX format
initial_type = [('float_input', FloatTensorType([None, X_train.shape[1]]))]
onnx_model = convert_sklearn(model, initial_types=initial_type)

# Saving the model
with open("decision_tree_model.onnx", "wb") as f:
    f.write(onnx_model.SerializeToString())
