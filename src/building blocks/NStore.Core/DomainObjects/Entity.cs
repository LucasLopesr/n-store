﻿using NStore.Core.Messages;
using System;
using System.Collections.Generic;

namespace NStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        private List<Event> notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => notificacoes?.AsReadOnly();

        public void AdicionarEvento(Event evento) 
        {
            notificacoes = notificacoes ?? new List<Event>();
            notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento) 
        {
            notificacoes?.Remove(evento);
        }

        public void LimparEventos() 
        {
            notificacoes?.Clear();
        }

        #region equals hashcode
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return true;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
        #endregion
    }
}
