export interface AccessTokenResponse {
  /**
   * The value is always "Bearer" which indicates this response provides a "Bearer" token.
   */
  tokenType: string;

  /**
   * The opaque bearer token to send as part of the Authorization request header.
   */
  accessToken: string;

  /**
   * The number of seconds before the accessToken expires.
   */
  expiresIn: number;

  /**
   * If set, this provides the ability to get a new access_token after it expires using a refresh endpoint.
   */
  refreshToken: string;
}
