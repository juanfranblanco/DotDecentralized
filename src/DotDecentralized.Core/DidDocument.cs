﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DotDecentralized.Core.Did
{
    /// <summary>
    /// https://w3c.github.io/did-core/
    /// </summary>
    [DebuggerDisplay("DidDocument(Id = {Id})")]
    public class DidDocument: IEquatable<DidDocument>
    {
        /// <summary>
        /// https://w3c.github.io/did-core/#did-subject
        /// </summary>
        [JsonPropertyName("@context")]
        public Context? Context { get; set; }

        //TODO: Add alsoKnownAs attribute. How is it modelled in the document? Continue the one GH thread.

        /// <summary>
        /// https://w3c.github.io/did-core/#did-subject
        /// </summary>
        [JsonPropertyName("id")]
        public Uri? Id { get; set; }

        //TODO: Make this a Controller class, maybe with implicit and explicit conversion to and from string. Same for some key formats?
        /// <summary>
        /// https://w3c.github.io/did-core/#control
        /// </summary>
        [JsonPropertyName("controller")]
        public string[]? Controller { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#verification-methods
        /// </summary>
        [JsonPropertyName("verificationMethod")]
        public VerificationMethod[]? VerificationMethod { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#authentication
        /// </summary>
        [JsonPropertyName("authentication")]
        public AuthenticationMethod[]? Authentication { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#assertionmethod
        /// </summary>
        [JsonPropertyName("assertionMethod")]
        public AssertionMethod[]? AssertionMethod { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#keyagreement
        /// </summary>
        [JsonPropertyName("keyAgreement")]
        public KeyAgreementMethod[]? KeyAgreement { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#capabilitydelegation
        /// </summary>
        [JsonPropertyName("capabilityDelegation")]
        public CapabilityDelegationMethod[]? CapabilityDelegation { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#capabilityinvocation
        /// </summary>
        [JsonPropertyName("capabilityInvocation")]
        public CapabilityInvocationMethod[]? CapabilityInvocation { get; set; }

        /// <summary>
        /// https://w3c.github.io/did-core/#service-endpoints
        /// </summary>
        [JsonPropertyName("service")]
        public Service[]? Service { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalData { get; set; }

        //TODO: The following as JSON Extension data + plus inherited from the converter?

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#created
        /// </summary>
        /*[JsonPropertyName("created")]
        public DateTimeOffset? Created { get; set; }

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#updated
        /// </summary>
        [JsonPropertyName("updated")]
        public DateTimeOffset? Updated { get; set; }*/


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if(obj is null)
            {
                return false;
            }

            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            if(GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((DidDocument)obj);
        }


        /// <inheritdoc/>
        public bool Equals(DidDocument other)
        {
            if(other == null)
            {
                return false;
            }

            return Context == other.Context
                && Id == other.Id
                && Controller.SequenceEqual(other.Controller)
                && VerificationMethod.SequenceEqual(other.VerificationMethod)
                && Authentication.SequenceEqual(other.Authentication)
                && AssertionMethod.SequenceEqual(other.AssertionMethod)
                && KeyAgreement.SequenceEqual(other.KeyAgreement)
                && CapabilityDelegation.SequenceEqual(other.CapabilityDelegation)
                && Service.SequenceEqual(other.Service);
        }


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Context);
            hash.Add(Id);

            for(int i = 0; i < Controller?.Length; ++i)
            {
                hash.Add(Controller[i]);
            }

            for(int i = 0; i < VerificationMethod?.Length; ++i)
            {
                hash.Add(VerificationMethod[i]);
            }

            for(int i = 0; i < Authentication?.Length; ++i)
            {
                hash.Add(Authentication[i]);
            }

            for(int i = 0; i < AssertionMethod?.Length; ++i)
            {
                hash.Add(AssertionMethod[i]);
            }

            for(int i = 0; i < KeyAgreement?.Length; ++i)
            {
                hash.Add(KeyAgreement[i]);
            }

            for(int i = 0; i < KeyAgreement?.Length; ++i)
            {
                hash.Add(KeyAgreement[i]);
            }

            for(int i = 0; i < CapabilityDelegation?.Length; ++i)
            {
                hash.Add(CapabilityDelegation[i]);
            }

            for(int i = 0; i < Service?.Length; ++i)
            {
                hash.Add(Service[i]);
            }

            return hash.ToHashCode();
        }
    }


    /// <summary>
    /// https://www.w3.org/TR/did-core/#service-endpoints
    /// </summary>
    [DebuggerDisplay("Service(Id = {Id})")]
    public class Service
    {
        [JsonPropertyName("id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("serviceEndpoint")]
        public string? ServiceEndPoint { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JsonElement>? AdditionalData { get; set; }
    }


    //TODO: These not as nameof-attributes with the same and small strings as in the standard!
    /// <summary>
    /// https://w3c.github.io/did-core/#key-types-and-formats
    /// </summary>
    public static class DidCoreKeyTypes
    {
        public const string RsaVerificationKey2018 = "rsaVerificationKey2018";
        public const string Ed25519VerificationKey2018 = "ed25519VerificationKey2018";
        public const string SchnorrSecp256k1VerificationKey2019 = "schnorrSecp256k1VerificationKey2019";
        public const string X25519KeyAgreementKey2019 = "x25519KeyAgreementKey2019";
    }

    /// <summary>
    /// https://www.w3.org/TR/did-spec-registries/#verification-method-types
    /// </summary>
    public static class DidRegisteredKeyTypes
    {
        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#jwsverificationkey2020
        /// </summary>
        public const string JwsVerificationKey2020 ="jwsVerificationKey2020";

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#ecdsasecp256k1verificationkey2019
        /// </summary>
        public const string EcdsaSecp256k1VerificationKey2019 = "ecdsaSecp256k1VerificationKey2019";

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#ed25519verificationkey2018
        /// </summary>
        public const string Ed25519VerificationKey2018 = "ed25519VerificationKey2018";

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#gpgverificationkey2020
        /// </summary>
        public const string GpgVerificationKey2020 = "gpgVerificationKey2020";

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#rsaverificationkey2018
        /// </summary>
        public const string RsaVerificationKey2018 = "rsaVerificationKey2018";

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#x25519keyagreementkey2019
        /// </summary>
        public const string X25519KeyAgreementKey2019 = "x25519KeyAgreementKey2019";

        /// <summary>
        /// https://www.w3.org/TR/did-spec-registries/#ecdsasecp256k1recoverymethod2020
        /// </summary>
        public const string EcdsaSecp256k1RecoveryMethod2020 = "ecdsaSecp256k1RecoveryMethod2020";
    }

    /// <summary>
    /// https://www.w3.org/TR/did-core/#key-types-and-formats
    /// </summary>
    public abstract class KeyFormat { }


    [DebuggerDisplay("PublicKeyHex({Key})")]
    public class PublicKeyHex: KeyFormat
    {
        public string Key { get; set; }

        public PublicKeyHex(string key)
        {
            Key = key ?? throw new ArgumentException(nameof(key));
        }
    }


    [DebuggerDisplay("PublicKeyBase58({Key})")]
    public class PublicKeyBase58: KeyFormat
    {
        public string Key { get; set; }

        public PublicKeyBase58(string key)
        {
            Key = key ?? throw new ArgumentException(nameof(key));
        }
    }


    [DebuggerDisplay("PublicKeyPem({Key})")]
    public class PublicKeyPem: KeyFormat
    {
        public string Key { get; set; }

        public PublicKeyPem(string key)
        {
            Key = key ?? throw new ArgumentException(nameof(key));
        }
    }


    [DebuggerDisplay("PublicKeyJwk(Crv = {Crv}, Kid = {Kid}, Kty = {Kty}, X = {X}, Y = {Y})")]
    public class PublicKeyJwk: KeyFormat
    {
        [JsonPropertyName("crv")]
        public string? Crv { get; set; }

        [JsonPropertyName("kid")]
        public string? Kid { get; set; }

        [JsonPropertyName("kty")]
        public string? Kty { get; set; }

        [JsonPropertyName("x")]
        public string? X { get; set; }

        [JsonPropertyName("y")]
        public string? Y { get; set; }
    }



    /// <summary>
    /// https://w3c.github.io/did-core/#verification-methods
    /// </summary>
    [DebuggerDisplay("VerificationMethod(Id = {Id})")]
    public class VerificationMethod
    {
        [JsonPropertyName("id")]
        public Uri? Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("controller")]
        public string? Controller { get; set; }

        public KeyFormat? KeyFormat { get; set; }
    }


    /// <summary>
    /// https://w3c.github.io/did-core/#verification-methods
    /// The reference Id field is string because it can be a fragment like "#key-1".
    /// </summary>
    [DebuggerDisplay("VerificationRelationship(Id = {Id}, IsEmbeddedVerification = {IsEmbeddedVerification})")]
    public abstract class VerificationRelationship
    {
        public string? VerificationReferenceId { get; }
        public VerificationMethod? EmbeddedVerification { get; }

        protected VerificationRelationship(string verificationReferenceId) => VerificationReferenceId = verificationReferenceId;
        protected VerificationRelationship(VerificationMethod embeddedVerification) => EmbeddedVerification = embeddedVerification;

        public string? Id => EmbeddedVerification == null ? VerificationReferenceId : EmbeddedVerification.Id?.ToString();

        public bool IsEmbeddedVerification { get { return EmbeddedVerification != null; } }
    }


    /// <summary>
    /// https://w3c.github.io/did-core/#verification-methods
    /// </summary>
    [DebuggerDisplay("AuthenticationMethod(Id = {Id}, IsEmbeddedVerification = {IsEmbeddedVerification})")]
    public class AuthenticationMethod: VerificationRelationship
    {
        public AuthenticationMethod(string verificationReferenceId) : base(verificationReferenceId) { }
        public AuthenticationMethod(VerificationMethod embeddedVerification) : base(embeddedVerification) { }
    }

    /// <summary>
    /// https://w3c.github.io/did-core/#verification-methods
    /// </summary>
    [DebuggerDisplay("AssertionMethod(Id = {Id}, IsEmbeddedVerification = {IsEmbeddedVerification})")]
    public class AssertionMethod: VerificationRelationship
    {
        public AssertionMethod(string verificationReferenceId) : base(verificationReferenceId) { }
        public AssertionMethod(VerificationMethod embeddedVerification) : base(embeddedVerification) { }
    }


    /// <summary>
    /// https://w3c.github.io/did-core/#verification-methods
    /// </summary>
    [DebuggerDisplay("KeyAgreementMethod(Id = {Id}, IsEmbeddedVerification = {IsEmbeddedVerification})")]
    public class KeyAgreementMethod: VerificationRelationship
    {
        public KeyAgreementMethod(string verificationReferenceId) : base(verificationReferenceId) { }
        public KeyAgreementMethod(VerificationMethod embeddedVerification) : base(embeddedVerification) { }
    }

    /// <summary>
    /// https://www.w3.org/TR/did-core/#verification-methods
    /// </summary>
    [DebuggerDisplay("CapabilityDelegationMethod(Id = {Id}, IsEmbeddedVerification = {IsEmbeddedVerification})")]
    public class CapabilityDelegationMethod: VerificationRelationship
    {
        public CapabilityDelegationMethod(string verificationReferenceId) : base(verificationReferenceId) { }
        public CapabilityDelegationMethod(VerificationMethod embeddedVerification) : base(embeddedVerification) { }
    }

    /// <summary>
    /// https://w3c.github.io/did-core/#verification-methods
    /// </summary>
    [DebuggerDisplay("CapabilityInvocationMethod(Id = {Id}, IsEmbeddedVerification = {IsEmbeddedVerification})")]
    public class CapabilityInvocationMethod: VerificationRelationship
    {
        public CapabilityInvocationMethod(string verificationReferenceId) : base(verificationReferenceId) { }
        public CapabilityInvocationMethod(VerificationMethod embeddedVerification) : base(embeddedVerification) { }
    }


    /// <summary>
    /// https://www.w3.org/TR/did-spec-registries/#context
    /// </summary>
    public class Context
    {
        public ICollection<string>? Contexes { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalData { get; set; }
    }
}