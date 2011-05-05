using System;
using System.Dynamic;
using Quantiv.Runtime.COM;

namespace QuantivContrib.Core.DynamicQuantiv
{
	public class QuantivEntityHelper
	{
		protected readonly Activity Activity;
		public Entity QuantivEntity { get; set; }
		public dynamic Attributes { get; set; }

		public object GetAttributeValue(string attributeRef)
		{
			return QuantivEntity.GetAttributeValue(attributeRef);
		}

		public void SetAttributeValue(string attributeRef, object value)
		{
			QuantivEntity.SetAttributeValue(attributeRef, value);
		}

		protected QuantivEntityHelper(Activity activity, Entity quantivEntity)
		{
			Activity = activity;
			QuantivEntity = quantivEntity;
			Attributes = new AttributeBag(quantivEntity);
		}

		protected static Entity CreateEntity(Activity activity, string entityRef)
		{
			return activity.GetEntityManager(entityRef).CreateEntity(true);
		}

		protected static Entity NewEntityWithManuallyAssignedId(Activity activity, string entityRef, string idAttributeRef, int idValue)
		{
			var entity = activity.GetEntityManager(entityRef).CreateEntity(false);
			entity.SetAttributeValue(idAttributeRef, idValue);
			return entity;
		}

		protected static T CreateEntity<T>(Activity activity, string entityRef) where T : QuantivEntityHelper
		{
			return ConvertTo<T>(activity, CreateEntity(activity, entityRef));
		}

		protected static T ConvertTo<T>(Activity activity, Entity entity)
		{
			return (T)Activator.CreateInstance(typeof(T), activity, entity);
		}

		protected static bool Exists(Activity activity, string entityRef, string attributeRef, object value, string retrievalPlan = "")
		{
			var quantivEntity = activity.GetEntityManager(entityRef).CreateEntity(false);
			if (!string.IsNullOrWhiteSpace(retrievalPlan))
			{
				quantivEntity.SetRetrievalPlan(retrievalPlan);
			}
			return quantivEntity.RetrieveWithAttributeValue(attributeRef, value);
		}

		protected static T Retrieve<T>(Activity activity, string entityRef, string attributeRef, object value, string retrievalPlan = "")
		{
			T result;
			var retrieved = TryRetrieve(activity, entityRef, attributeRef, value, out result, retrievalPlan);
			if (!retrieved)
			{
				throw new Exception(string.Format("Failed to retrieve entity '{0}' using attribute '{1}' and value '{2}'.", entityRef, attributeRef, value));
			}
			return result;
		}

		protected static bool TryRetrieve<T>(Activity activity, string entityRef, string attributeRef, object value, out T record, string retrievalPlan = "")
		{
			var quantivEntity = activity.GetEntityManager(entityRef).CreateEntity(false);
			if (!string.IsNullOrWhiteSpace(retrievalPlan))
			{
				quantivEntity.SetRetrievalPlan(retrievalPlan);
			}

			var success = quantivEntity.RetrieveWithAttributeValue(attributeRef, value);
			record = ConvertTo<T>(activity, quantivEntity);
			return success;
		}

		protected static int AllocateCounterId(Activity activity, string entityRef, string counterName, string counterSubRef = "")
		{
			return Int32.Parse(activity.GetEntityManager(entityRef).GetCounter(counterName, counterSubRef).GetNextValue().ToString());
		}

		public void Save()
		{
			QuantivEntity.Save(Activity);
		}

		/// <summary>
		/// This is far and away the most interesting thing in this project.
		/// Removes the horrible GetAttributeValue stuff and replaces them with dynamics.
		/// </summary>
		private class AttributeBag : DynamicObject
		{
			private readonly Entity _quantivEntity;

			public AttributeBag(Entity quantivEntity)
			{
				_quantivEntity = quantivEntity;
			}

			public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
			{
				if (args.Length == 0)
				{
					var value = _quantivEntity.GetAttributeValue(binder.Name);
					result = value;
					return true;
				}

				_quantivEntity.SetAttributeValue(binder.Name, args[0]);
				result = args[0];
				return true;
			}
		}

	}
}