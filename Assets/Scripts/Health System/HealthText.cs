public static class HealthText {
    public static string GetText(Health health) {
        return health.CurrentHealth + "/" + health.MaxHealth;
    }
}
